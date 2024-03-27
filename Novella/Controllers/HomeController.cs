using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Novella.EfModels;
using System.Diagnostics;
using Novella.Models;
using Novella.Repositories;
using System.Security.Claims;
using Novella.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;


namespace Novella.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepo _productRepo;
        private readonly NovellaContext _db;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserRepo _userRepo;

        public HomeController(ILogger<HomeController> logger,
                              NovellaContext db,
                              IConfiguration configuration,
                              ProductRepo productRepo, 
                              IWebHostEnvironment webHostEnvironment,
                              UserManager<IdentityUser> userManager,
                              UserRepo userRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
            _configuration = configuration;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userRepo = userRepo;
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
        public async Task<IActionResult> SubmitRating(int productId, decimal rating, string review)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirect unauthenticated users to the login page
                return Redirect("/Identity/Account/Login");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the UserName using UserRepo and UserManager
            var userName = await _userRepo.GetUserNameByUserIdAsync(userId);

            if (userName != null)
            {
                var success = _productRepo.SubmitRating(userName, productId, rating, review);

                if (success)
                {
                    // Rating submitted successfully
                    return RedirectToAction("Detail", new { productId });
                }
            }

            // Handle rating submission failure
            return RedirectToAction("Error", "Home");
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
        public IActionResult AddToCart(int ProductId, int Quantity)
        {
            // Your logic to handle adding the product to the cart goes here
            // Assuming you're using Session to store the cart
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
    
            // Find the product by ProductId
            var product = _productRepo.GetProductById(ProductId.ToString()); // Adjust based on your method's requirements
    
            if(product != null)
            {
                // Check if the product already exists in the cart
                var cartItem = cart.Find(c => c.ProductId == ProductId.ToString());
                if(cartItem != null)
                {
                    // If exists, just update the quantity
                    cartItem.Quantity += Quantity;
                }
                else
                {
                    // Add new item to the cart
                    cart.Add(new CartItem 
                    { 
                        ProductId = ProductId.ToString(), 
                        ProductName = product.ProductName, 
                        Price = product.Price, 
                        Quantity = Quantity 
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
    }
}
