using Microsoft.AspNetCore.Mvc;
using Novella.Data;
using Novella.Repositories;
using Novella.ViewModels;
using System;
using Microsoft.Extensions.Logging;
using Novella.EfModels;

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

        // Display product edit form
        //public IActionResult Edit(int productId) // Assuming ProductId is int; adjust if it's a different type
        //{
        //    var product = _productRepo.GetProductById(productId);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        // Handle product edit form submission
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

        // Display delete confirmation
        //public IActionResult Delete(int productId) // Adjust if ProductId is a different type
        //{
        //    var product = _productRepo.GetProductById(productId);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //// Handle product deletion
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int productId) // Adjust if ProductId is a different type
        //{
        //    var success = _productRepo.DeleteProduct(productId);
        //    if (success)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
