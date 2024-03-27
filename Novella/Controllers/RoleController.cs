using Novella.Data;
using Novella.Models;
using Novella.Repositories;
using Novella.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Novella.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RoleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            RoleRepo roleRepo = new RoleRepo(_db);
            var roles = roleRepo.GetAllRoles();
            return View(roles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_db);
                bool isSuccess = roleRepo.CreateRole(roleVM.RoleName);

                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Role creation failed." +
                                             " The role may already exist.");
                }
            }
            return View(roleVM);
        }

        [HttpGet]
        public IActionResult Delete(string roleName)
        {
            RoleRepo roleRepo = new RoleRepo(_db);
            var result = roleRepo.DeleteRole(roleName);

            TempData["Message"] = result;
            return RedirectToAction(nameof(Index));
        }


    }
}
