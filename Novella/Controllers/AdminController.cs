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
        //        try
        //        {
        //            // Create a new Product entity
        //            var product = new Product
        //            {
        //                Price = productVM.Price,
        //                ProductName = productVM.ProductName,
        //                ProductDescription = productVM.ProductDescription,
        //                QuantityAvailable = productVM.QuantityAvailable,
        //                FkCategoryId = productVM.CategoryId
        //            };

        //            // Add the product to the repository
        //            Product newProduct = _productRepo.AddProduct(product);

        //            // Check if an image file was provided
        //            if (productVM.ImageFile != null && productVM.ImageFile.Length > 0)
        //            {
        //                // Read the file contents into a byte array
        //                byte[] imageData;
        //                using (var ms = new MemoryStream())
        //                {
        //                    productVM.ImageFile.CopyTo(ms);
        //                    imageData = ms.ToArray();
        //                }

        //                // Create a new ImageStore entity for the uploaded image
        //                var imageStore = new ImageStore
        //                {
        //                    FileName = productVM.ImageFile.FileName,
        //                    Image = imageData,
        //                    FkProductId = newProduct.PkProductId.Value

        //                };

        //                // Add the image store to the repository
        //                bool success = _productRepo.AddImage(imageStore);
        //                if (success)
        //                {
        //                    // Redirect to a confirmation page
        //                    return RedirectToAction("Index");
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("", "There was a problem saving the product image. Please try again.");
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Please select an image file.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception message
        //            Console.WriteLine("Error adding product: " + ex.Message);
        //            ModelState.AddModelError("", "There was an unexpected error adding the product. Please try again later.");
        //        }
        //    }

        //    // failed, redisplay form
        //    return View(productVM);
        //}



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
           
                        // If product added successfully, proceed to image handling
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
                                FkProductId = newProduct.PkProductId.Value 
                            };

                            bool imageSaveSuccess = _productRepo.AddImage(imageStore);
                            if (!imageSaveSuccess)
                            {
                                return RedirectToAction("Index"); // Redirect to product list after successful creation
                            }
                        }
  
                    else
                    {
                        ModelState.AddModelError("", "There was a problem saving the product. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine("Error adding product: " + ex.Message);
                ModelState.AddModelError("", "There was an unexpected error adding the product. Please try again later.");
            }

            // failed; redisplay form
            return View(productVM);
        }



        [HttpGet, ActionName("Details")]
        public IActionResult Details(int id)
        {
            // first get product
            int productId = id;
            var product = _productRepo.GetProductById(productId.ToString());
            return View(product);
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
                // Optionally, add a model error or a flash message indicating failure
                return View("Error", new ErrorViewModel { RequestId = "DeleteFailed" }); // Ensure you have an ErrorViewModel and view
            }
        }
    }
}
