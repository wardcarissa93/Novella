using Microsoft.AspNetCore.Mvc;
using Novella.EfModels;
using System.Diagnostics;
using Novella.Models;
using Novella.Repositories;

namespace Novella.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepo _productRepo;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
                              NovellaContext db,
                              IConfiguration configuration)
        {
            _logger = logger;
            _productRepo = new ProductRepo(db);
            _configuration = configuration;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
