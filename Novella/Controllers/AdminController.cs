using Microsoft.AspNetCore.Mvc;
using Novella.Data;
using Novella.Repositories;
using Novella.ViewModels;
using System;
using Microsoft.Extensions.Logging;
using Novella.EfModels;
using Novella.Models;
using Microsoft.Extensions.Hosting;

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
            return View("ProductIndex",products);
        }

        public IActionResult Edit(int id)
        {
            var productVM = _productRepo.GetProductVMById(id.ToString());
            if (productVM == null)
            {
                return NotFound();
            }

            return View("ProductEdit",productVM);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, ProductAdminVM productVM, List<IFormFile> newImages)
        //{
        //    if (id != productVM.ProductId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Handle new image uploads
        //        foreach (var file in newImages)
        //        {
        //            if (file.Length > 0)
        //            {
        //                // Generate a unique file name
        //                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //                }

        //                productVM.NewImageFilenames.Add(fileName); // Add the file name to the list
        //            }
        //        }

        //        bool success = _productRepo.EditProduct(productVM);
        //        if (success)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Unable to save changes.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(productVM);
        //}

        public IActionResult Create()
        {
            return View("ProductCreate",new ProductVM());  
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
                            FkProductId = newProduct.PkProductId.Value
                        };

                        bool imageSaveSuccess = _productRepo.AddImage(imageStore);
                        if (!imageSaveSuccess)
                        {
                            // Log the error or handle it as needed
                            Console.WriteLine($"Failed to save image for product {newProduct.ProductName}");
                            // Consider whether to proceed with redirecting despite image save failure
                        }
                    }

                    // Redirect to "Index" after successful product and optional image addition
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
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
            var product = _productRepo.GetProductVMById(productId.ToString());
            return View("ProductDetails",product);
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
