using Microsoft.AspNetCore.Mvc;
using Novella.EfModels;
using System.Diagnostics;
using Novella.Models;
using Novella.Repositories;
using System.Security.Claims;
using Newtonsoft.Json;


namespace Novella.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepo _productRepo;
        private readonly NovellaContext _db;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger,
                              NovellaContext db,
                              IConfiguration configuration,
                              ProductRepo productRepo, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _productRepo = productRepo;
            _configuration = configuration;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        

        public IActionResult Index()
        {
            var top3Products = _productRepo.GetTop3ProductsWithHighestRatings();
            return View(top3Products);
        }

        public IActionResult Detail(int productId, string imageUrl, int page = 1)
        {
            // Get the product details by ID
            var product = _productRepo.GetProductById(productId.ToString());

            if (product == null)
            {
                // Handle the null case, e.g., return a not found view or set an error message
                return NotFound();
            }


            // Pass the product details to the view
            ViewBag.ProductName = product.ProductName;
            ViewBag.Price = product.Price;
            ViewBag.ProductId = productId;
            ViewBag.ProductDescription = product.ProductDescription;
            ViewBag.Rating = product.Rating;
            ViewBag.ImageUrl = imageUrl;
            // Pagination
            int pageSize = 1;
            int skip = (page - 1) * pageSize;

            ViewBag.Reviews = product.Reviews
                                    .Skip(skip)
                                    .Take(pageSize)
                                    .ToList();
            ViewBag.PageCount = (int)Math.Ceiling((double)product.Reviews.Count / pageSize);
            ViewBag.CurrentPage = page;

            return View(product);
        }

        [HttpPost]
        public IActionResult SubmitRating(int productId, decimal rating, string review)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirect unauthenticated users to the login page
                return Redirect("/Identity/Account/Login");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = _productRepo.SubmitRating(userId, productId, rating, review);

            if (success)
            {
                // Rating submitted successfully
                return RedirectToAction("Detail", new { productId });
            }
            else
            {
                // Handle rating submission failure
                return RedirectToAction("Error", "Home");
            }
        }

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

        public IActionResult Search(string query)
        {
            var searchResults = _productRepo.SearchProducts(query, productId =>
                Url.Action("GetImage", "Home", new { productId = productId }));

            return View("Search", searchResults);
        }

        public IActionResult CheckOut()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var totalPrice = cart.Sum(item => item.Price * item.Quantity);

            var model = new PayPalConfirmationModel
            {
                TotalPrice = totalPrice
            };
            ViewBag.Cart = cart;
            ViewBag.PayPalClientID = _configuration["PayPal:ClientID"];
            return View(model);
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
        public IActionResult AddToCart(int productId, int quantity, string size, string color)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Find the product by ProductId
            var product = _productRepo.GetProductById(productId.ToString());

            if (product != null)
            {
                // Check if the product already exists in the cart
                var cartItem = cart.Find(c => c.ProductId == productId.ToString());
                if (cartItem != null)
                {
                    // If exists, just update the quantity
                    cartItem.Quantity += quantity;
                }
                else
                {
                    // Add new item to the cart
                    cart.Add(new CartItem
                    {
                        ProductId = productId.ToString(),
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        Price = product.Price,
                        Quantity = quantity,
                        Size = size,
                        Color = color
                    });
                }

                // Save the updated cart back to the session
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            // Redirect to the Cart view
            return RedirectToAction("Cart");
        }


        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult UpdateCartIcon()
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Calculate total quantity
            int totalQuantity = cart.Sum(item => item.Quantity);

            // Store the total quantity in session storage
            HttpContext.Session.SetInt32("TotalQuantity", totalQuantity);

            // Return the total quantity
            return Json(totalQuantity);
        }

        public int GetTotalQuantityInCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            int totalQuantity = cart.Sum(item => item.Quantity);
            return totalQuantity;
        }


        [HttpPost]
        public JsonResult UpdateQuantity(int productId, int change)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId.ToString());
            if (item != null)
            {
                item.Quantity += change;
                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    // Optionally, add logic here to update the item price based on the new quantity
                    // For example, if you have quantity discounts
                }

                HttpContext.Session.SetObjectAsJson("Cart", cart);

                // Optionally, calculate and return the updated cart summary to update the UI
                var subtotal = cart.Sum(c => c.Price * c.Quantity);
                var tax = subtotal * 0.05m; // Assuming a fixed tax rate of 5%
                var total = subtotal + tax;

                return Json(new { success = true, subtotal = subtotal, tax = tax, total = total, count = cart.Count });
            }

            return Json(new { success = false, message = "Item not found in cart." });
        }

        [HttpGet]
        public JsonResult GetCartSummary()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var subtotal = cart.Sum(c => c.Price * c.Quantity);
            var tax = subtotal * 0.05m; // Assuming a fixed tax rate of 5%
            var total = subtotal + tax;

            return Json(new { subtotal = subtotal, tax = tax, total = total, count = cart.Count });
        }





    }
}