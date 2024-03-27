using Microsoft.AspNetCore.Mvc;
using Novella.Data;
using Novella.Repositories;
using Novella.ViewModels;
using System;
using Microsoft.Extensions.Logging;
using Novella.EfModels;
using Novella.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Novella.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ProductRepo _productRepo;


        public AdminController(ILogger<AdminController> logger,
                               ProductRepo productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        // Display list of products
        public IActionResult Index()
        {
            var products = _productRepo.GetProductsForAdmin();
            return View("ProductIndex", products);
        }

        public IActionResult Edit(int id)
        {
            var productVM = _productRepo.GetProductById(id.ToString());
            if (productVM == null)
            {
                return NotFound();
            }

            return View("ProductEdit", productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var result = _productRepo.EditProduct(productVM);
                if (result)
                {
                    // Redirect to the index view or display a success message
                    return RedirectToAction("Index");
                }
                else
                {
                    // Log error, and let the user know the update didn't work
                    ModelState.AddModelError("", "An error occurred while updating the product. Please try again.");
                }
            }
            else
            {
                Console.WriteLine(ModelState);
            }

            // If we got this far, something failed, redisplay the form
            return View("ProductEdit", productVM);
        }

        public IActionResult Create()
        {
            return View("ProductCreate", new ProductVM());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        ProductName = productVM.ProductName,
                        Price = productVM.Price,
                        ProductDescription = productVM.ProductDescription,
                        QuantityAvailable = productVM.QuantityAvailable,
                        FkCategoryId = productVM.CategoryId
                    };
                    Product newProduct = _productRepo.AddProduct(product);

                    // If there's an image file, process it
                    if (productVM.ImageFile != null && productVM.ImageFile.Length > 0)
                    {
                        byte[] imageData;
                        using (var ms = new MemoryStream())
                        {
                            productVM.ImageFile.CopyTo(ms);
                            imageData = ms.ToArray();
                        }

                        var imageStore = new ImageStore
                        {
                            FileName = productVM.ImageFile.FileName,
                            Image = imageData,
                            FkProductId = newProduct.PkProductId
                        };

                        bool imageSaveSuccess = _productRepo.AddImage(imageStore);
                        if (!imageSaveSuccess)
                        {
                            // Log the error or handle it as needed
                            Console.WriteLine($"Failed to save image for product {newProduct.ProductName}");
                            // Consider whether to proceed with redirecting despite image save failure
                        }
                    }

                    var products = _productRepo.GetProductsForAdmin();
                    return View("ProductIndex", products);
                }
            }
            catch (Exception ex)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        // Log or inspect the error message
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                // Log the exception message
                Console.WriteLine($"Error adding product: {ex.Message}");
                ModelState.AddModelError("", "There was an unexpected error adding the product. Please try again later.");
            }

            // If model state is invalid, an exception occurred, or image saving failed without redirecting, stay on form
            return View("ProductCreate", productVM);
        }


        [HttpGet, ActionName("Details")]
        public IActionResult Details(int id)
        {
            // first get product
            int productId = id;
            var product = _productRepo.GetProductById(productId.ToString());
            return View("ProductDetails", product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            bool success = _productRepo.DeleteProduct(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "DeleteFailed" });
            }
        }
    }
}
