using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.Owin;
using QRMenu_Mvc.Models;
using QRMenu_Mvc.Data;

using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace QRMenu_Mvc.Controllers
{
    [Authorize(Roles = "Admin, BrandAdmin")]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AppRolesController(RoleManager<AppRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Roles
        public async Task<ActionResult> Index()
        {
           // return View();
            return _roleManager.Roles.Any() ?
                View(await _roleManager.Roles.ToListAsync()) :
                Problem("No roles found.");
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View();
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string Name)
        {
            AppRole appRole = new AppRole(Name); // daha kısa yolla yapabiliriz, diğer yöntemle yap ya da approle sil 
            await _roleManager.CreateAsync(appRole);
            return RedirectToAction ("Index");

        }
        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(new
            {
                RoleId = role.Id,
                RoleName = role.Name
            });
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string roleName)
        {
            if (id != roleName)
            {
                return BadRequest();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = roleName;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(new
            {
                RoleId = role.Id,
                RoleName = role.Name
            });
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(new
            {
                RoleId = role.Id,
                RoleName = role.Name
            });
        }
    }
}
