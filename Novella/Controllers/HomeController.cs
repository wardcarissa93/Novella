﻿using Microsoft.AspNetCore.Mvc;
using Novella.EfModels;
using System.Diagnostics;
using Novella.Models;
using Novella.Repositories;
using Novella.Services;

namespace Novella.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepo _productRepo;
        private readonly NovellaContext _db;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
                              NovellaContext db,
                              IConfiguration configuration)
        {
            _logger = logger;
            _productRepo = new ProductRepo(db);
            _configuration = configuration;
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetProductsWithReviewsForHome();
            return View(products);
          
        }

        public IActionResult Detail(string productName, decimal price, string description)
        {
            ViewBag.ProductName = productName;
            ViewBag.Price = price;
            ViewBag.Description = description;
            
            return View();
        }

        public IActionResult Pendant(string productName, decimal price, string description)
        {
            ViewBag.ProductName = productName;
            ViewBag.Price = price;
            ViewBag.Description = description;

            var products = _productRepo.GetProductsForPendant();
            return View(products);
        }

        public IActionResult Choker()
        {
            var products = _productRepo.GetProductsForChoker();
            return View(products);
        }
        public IActionResult Chain()
        {
            var products = _productRepo.GetProductsForChain();
            return View(products);
        }



        public IActionResult CheckOut()
        {

            ViewBag.PayPalClientID = _configuration["PayPal:ClientID"];
            return View();
        }



        public IActionResult PayPalConfirmation(PayPalConfirmationModel payPalConfirmationModel)

        {

            return View(payPalConfirmationModel);

        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Images()
        {
            IEnumerable<ImageStore> images = _db.ImageStores;

            return View(images);
        }

        public IActionResult SaveImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveImage(UploadModel uploadModel)
        {
            if (ModelState.IsValid)
            {
                if (uploadModel.ImageFile != null && uploadModel.ImageFile.Length > 0)
                {
                    string contentType = uploadModel.ImageFile.ContentType;

                    if (contentType == "image/png" ||
                    contentType == "image/jpeg" ||
                    contentType == "image/jpg")
                    {
                        try
                        {
                            byte[] imageData;

                            using (var memoryStream = new MemoryStream())
                            {
                                await uploadModel.ImageFile.CopyToAsync(memoryStream);
                                imageData = memoryStream.ToArray();
                            }

                            var image = new ImageStore
                            {
                                FileName = Path.
                            GetFileNameWithoutExtension(uploadModel.ImageFile.FileName),
                                Image = imageData
                            };

                            _db.ImageStores.Add(image);
                            await _db.SaveChangesAsync();

                            return RedirectToAction("Index", "Images");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("imageUpload"
                            , "An error occured uploading your image."
                            + " Please try again.");
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("imageUpload", "Please upload a PNG, " +
                        "JPG, or JPEG file.");
                    }
                }
                else
                {
                    ModelState.AddModelError("imageUpload", "Please select an " +
                    " image to upload.");
                }
            }

            return View(uploadModel);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem cartItem)
        {
            // Assuming you're using Session to store the cart
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.Add(cartItem);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new { success = true, message = "Product added to cart successfully." });
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }



    }
}
