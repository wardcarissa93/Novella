using Microsoft.AspNetCore.Mvc;
using Novella.EfModels;
using System.Diagnostics;
using Novella.Models;
using Novella.Repositories;
using Novella.Services;
using Microsoft.AspNetCore.Hosting;

namespace Novella.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepo _productRepo;
        private readonly NovellaContext _db;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger,
                              NovellaContext db,
                              IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _productRepo = new ProductRepo(db);
            _configuration = configuration;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetProductsWithReviewsForHome();
            return View(products);
          
        }

        public IActionResult Detail(string productName, decimal price, string description, string imageUrl)
        {
            ViewBag.ProductName = productName;
            ViewBag.Price = price;
            ViewBag.Description = description;
            ViewBag.ImageUrl = imageUrl;
            return View();
        }

        //public IActionResult Pendant(string productName, decimal price, string description)
        //{
        //    ViewBag.ProductName = productName;
        //    ViewBag.Price = price;
        //    ViewBag.Description = description;

        //    var products = _productRepo.GetProductsForPendant();
        //    return View(products);
        //}

        public IActionResult Pendant(string productName, decimal price, string description)
        {
            ViewBag.ProductName = productName;
            ViewBag.Price = price;
            ViewBag.Description = description;

            var products = _productRepo.GetProductsForPendant();

            // Fetch images for the products
            foreach (var product in products)
            {
                product.ImageUrl = Url.Action("GetImage", "Home", new { productId = product.ProductId });
            }

            return View(products);
        }
        

        public IActionResult Choker()
        {
            var products = _productRepo.GetProductsForChoker();
            // Fetch images for the products
            foreach (var product in products)
            {
                product.ImageUrl = Url.Action("GetImage", "Home", new { productId = product.ProductId });
            }
            return View(products);
        }
        public IActionResult Chain()
        {
            var products = _productRepo.GetProductsForChain();
            // Fetch images for the products
            foreach (var product in products)
            {
                product.ImageUrl = Url.Action("GetImage", "Home", new { productId = product.ProductId });
            }
            return View(products);
        }



        public IActionResult CheckOut()
        {

            ViewBag.PayPalClientID = _configuration["PayPal:ClientID"];
            return View();
        }

        public FileResult GetImage(int productId)
        {
            var image = _db.ImageStores.FirstOrDefault(i => i.FkProductId == productId);
            if (image != null)
            {
                return File(image.Image, "image/jpeg");
            }
            else
            {
                // Return a default image or handle the case where the image is not found

                var defaultImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "404_img.jpeg");
                return File(defaultImagePath, "image/jpeg");
            }
        }



        public IActionResult PayPalConfirmation(PayPalConfirmationModel payPalConfirmationModel)

        {

            return View(payPalConfirmationModel);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
