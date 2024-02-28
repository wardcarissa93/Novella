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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(ProductVM productVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var product = new Product
        //        {
        //            // Assuming your Product entity matches these properties
        //            ProductName = productVM.ProductName,
        //            Price = productVM.Price,
        //            ProductDescription = productVM.ProductDescription,
        //            QuantityAvailable = productVM.QuantityAvailable,
        //            FkCategoryId = productVM.CategoryId
        //        };

        //        bool success = _productRepo.AddProduct(product);
        //        if (success)
        //        {
        //            // Redirect to a confirmation page, product list, or another appropriate action
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "There was a problem saving the product. Please try again.");
        //        }
        //    }

        //    // If we got this far, something failed; redisplay form
        //    return View(productVM);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                // Check if an image file was provided
                if (productVM.ImageFile != null && productVM.ImageFile.Length > 0)
                {
                    // Read the file contents into a byte array
                    byte[] imageData;
                    using (var ms = new MemoryStream())
                    {
                        productVM.ImageFile.CopyTo(ms);
                        imageData = ms.ToArray();
                    }

                    // Create a new ImageStore entity for the uploaded image
                    var imageStore = new ImageStore
                    {
                        FileName = productVM.ImageFile.FileName, // Assuming you want to store the file name
                        Image = imageData, // Set image data property
                                           // Assuming you have a way to determine the product ID
                        FkProductId = productVM.ProductId // You may need to adjust this depending on how you retrieve or determine the product ID
                    };

                    // Attempt to add the image store to the repository
                    bool success = _productRepo.AddImage(imageStore); // Adjust this based on your repository method
                    if (success)
                    {
                        // Redirect to a confirmation page, product list, or another appropriate action
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "There was a problem saving the product image. Please try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please select an image file.");
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
