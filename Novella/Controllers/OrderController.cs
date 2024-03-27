using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novella.ViewModels;
using Novella.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Novella.EfModels;

namespace Novella.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepo _orderRepo;
        private readonly AddressRepo _addressRepo;
        private readonly ProductOrderRepo _productOrderRepo;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(OrderRepo orderRepo, ProductOrderRepo productOrderRepo,AddressRepo addressRepo, ILogger<OrderController> logger, UserManager<IdentityUser> userManager)
        {
            _orderRepo = orderRepo;
            _addressRepo = addressRepo;
            _logger = logger;
            _userManager = userManager;
            _productOrderRepo = productOrderRepo;
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody] OrderVM orderVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid order data");
                }

                // Retrieve the email ID of the currently logged-in user
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }
                var email = user.Email;

                // Extract shipping address from OrderVM
                var shippingAddress = orderVM.ShippingAddress;

                // Extract billing address from OrderVM
                var billingAddress = orderVM.BillingAddress;

                // Create Order object
                var order = new Order
                {
                    FkUserId = email,
                    FkOrderStatusId = 1,  
                    FkShippingAddressId = _addressRepo.SaveAddressAndGetId(shippingAddress),
                    FkBillingAddressId = _addressRepo.SaveAddressAndGetId(billingAddress),
                    DateOrdered = DateTime.Now,
                    PaypalTransactionId = orderVM.PaypalTransactionId
                };

                // Save the order
                var orderId = _orderRepo.SaveOrderAndGetId(order);

                // Check if order is successfully saved
                if (orderId > 0) 
                {
                    foreach (var cartItem in orderVM.CartItems)
                    {
                        var productOrder = new ProductOrder
                        {
                            FkOrderId = orderId,
                            FkProductId = int.Parse(cartItem.ProductId),  
                            QuantityInOrder = cartItem.Quantity
                        };

                        _productOrderRepo.SaveProductOrder(productOrder);
                    }
                }

                var redirectUrl = Url.Action("OrderConfirmation", "Order", new { id = orderId });
                return Json(new { success = true, redirectUrl = redirectUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving order");
                return Json(new { success = false, message = "Order could not be saved." });
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
