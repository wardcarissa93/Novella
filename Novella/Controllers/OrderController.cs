using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novella.ViewModels;
using Novella.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Novella.EfModels;
using Newtonsoft.Json;
using Novella.Models;
using Microsoft.EntityFrameworkCore;

namespace Novella.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepo _orderRepo;
        private readonly AddressRepo _addressRepo;
        private readonly ProductOrderRepo _productOrderRepo;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly NovellaContext _context;



        public OrderController(OrderRepo orderRepo, ProductOrderRepo productOrderRepo, AddressRepo addressRepo, ILogger<OrderController> logger, UserManager<IdentityUser> userManager, NovellaContext context)
        {
            _orderRepo = orderRepo;
            _addressRepo = addressRepo;
            _logger = logger;
            _userManager = userManager;
            _productOrderRepo = productOrderRepo;
            _context = context;
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
            var order = _context.Orders.Include(o => o.FkShippingAddress).Include(o => o.FkBillingAddress).FirstOrDefault(o => o.PkOrderId == id);
            var orderStatus = _context.OrderStatuses.Find(order?.FkOrderStatusId);


            // Retrieve the cart from the session
            var cart = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);

            var formattedShippingAddress = FormatAddress(order.FkShippingAddress);
            var formattedBillingAddress = FormatAddress(order.FkBillingAddress);

            var viewModel = new OrderConfirmationViewModel
            {
                OrderId = id,
                PaypalTransactionId = order.PaypalTransactionId,
                OrderStatus = _context.OrderStatuses.Find(order.FkOrderStatusId)?.StatusName ?? "Unknown",
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("Cart") ?? "[]"),
                PaymentMethod = "PayPal", // Example, adjust accordingly
                ShippingAddress = formattedShippingAddress,
                BillingAddress = formattedBillingAddress,
                EstimatedDelivery = DateTime.Now.AddDays(5).ToString("dd MMM yyyy") // Example, adjust accordingly
            };
            // Clear the cart after the view model has been populated
            HttpContext.Session.SetObjectAsJson("Cart", new List<CartItem>());
            return View(viewModel);
        }
        private string FormatAddress(Address address)
        {
            // This method formats the address details into a single string for display.
            // Adjust according to your Address model's properties.
            var userAddress = $"{address.AddressLineOne}, {address.AddressLineTwo}, {address.City}, {address.Province}, {address.PostalCode}";
            return userAddress;
        }
    }
}