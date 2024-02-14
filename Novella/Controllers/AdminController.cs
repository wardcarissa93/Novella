using Microsoft.AspNetCore.Mvc;
using Novella.Data;
using Novella.EfModels;
using Novella.Repositories;  
using Novella.ViewModels;  

namespace Novella.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;  
        private readonly ProductRepo _productRepo;
        private readonly NovellaContext _db;

        public AdminController(ILogger<AdminController> logger, NovellaContext db)
        {
            _logger = logger;
            _db = db;
        }

        // Display list of products
        public IActionResult Index()
        {
            ProductRepo _productRepo = new ProductRepo(_db);
            var products = _productRepo.GetProductsForAdmin();
            return View(products);
        }

        // Display product edit form
        public IActionResult Edit(string productId)
        {
            ProductRepo _productRepo = new ProductRepo(_db);
            var product = _productRepo.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Handle product edit form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductAdminVM productVM)
        {
            ProductRepo _productRepo = new ProductRepo(_db);
            if (ModelState.IsValid)
            {
                var success = _productRepo.EditProduct(productVM);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Product could not be updated.");
                }
            }
            return View(productVM);
        }

        // Display delete confirmation
        public IActionResult Delete(string productId)
        {
            ProductRepo _productRepo = new ProductRepo(_db);
            var product = _productRepo.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Handle product deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string productId)
        {
            ProductRepo _productRepo = new ProductRepo(_db);
            var success = _productRepo.DeleteProduct(productId);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
