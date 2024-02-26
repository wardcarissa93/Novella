using Microsoft.AspNetCore.Mvc;
using Novella.Data;
using Novella.Repositories;
using Novella.ViewModels;
using System;
using Microsoft.Extensions.Logging;
using Novella.EfModels;
using Novella.Models;

namespace Novella.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ProductRepo _productRepo;

        public AdminController(ILogger<AdminController> logger, NovellaContext db)
        {
            _logger = logger;
            _productRepo = new ProductRepo(db);
        }

        // Display list of products
        public IActionResult Index()
        {
            var products = _productRepo.GetProductsForAdmin();
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            var productVM = _productRepo.GetProductById(id.ToString());
            if (productVM == null)
            {
                return NotFound();
            }

            return View(productVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductAdminVM productVM)
        {
            if (ModelState.IsValid)
            {
                bool success = int.TryParse(productVM.ProductId, out int productId); // Ensure this matches your model
                if (success)
                {
                    success = _productRepo.EditProduct(productVM);
                    if (success)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Product could not be updated.");
                    }
                }
            }
            return View(productVM);
        }

        public IActionResult Create()
        {
            return View(new ProductVM());  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    // Assuming your Product entity matches these properties
                    ProductName = productVM.ProductName,
                    Price = productVM.Price,
                    ProductDescription = productVM.ProductDescription,
                    QuantityAvailable = productVM.QuantityAvailable,
                    FkCategoryId = productVM.CategoryId
                };

                bool success = _productRepo.AddProduct(product);
                if (success)
                {
                    // Redirect to a confirmation page, product list, or another appropriate action
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There was a problem saving the product. Please try again.");
                }
            }

            // If we got this far, something failed; redisplay form
            return View(productVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool success = _productRepo.DeleteProduct(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Optionally, add a model error or a flash message indicating failure
                return View("Error", new ErrorViewModel { RequestId = "DeleteFailed" }); // Ensure you have an ErrorViewModel and view
            }
        }

    }
}
