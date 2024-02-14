using Microsoft.AspNetCore.Mvc;
using Novella.EfModels;
using Novella.Models;
using System.Diagnostics;

namespace Novella.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NovellaContext _db;

        public HomeController(ILogger<HomeController> logger,
                              NovellaContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Pendant()
        {
            return View();
        }

        public IActionResult Choker()
        {
            return View();
        }
        public IActionResult Chain()
        {
            return View();
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
