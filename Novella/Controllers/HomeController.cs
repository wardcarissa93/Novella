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

        public HomeController(ILogger<HomeController> logger,
                              NovellaContext db)
        {
            _logger = logger;
            _productRepo = new ProductRepo(db);
         
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetProductsForHome();
            return View(products);
           

        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Pendant()
        {
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
