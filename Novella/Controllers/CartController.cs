using Microsoft.AspNetCore.Mvc;
using Novella.EfModels;
using Novella.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Novella.Controllers
{
    public class CartController : Controller
    {
        private readonly NovellaContext _context;

        public CartController(NovellaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult RemoveItem(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId.ToString());
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObjectAsJson("Cart", cart);

            }
            return RedirectToAction("Cart", "Home");
        }

        [HttpPost]
        public IActionResult UpdateQuantity([FromBody] UpdateQuantityModel model)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == model.ProductId);
            if (item != null)
            {
                item.Quantity += model.Change;
                if (item.Quantity <= 0) cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Item not found in cart." });
        }

    }
}