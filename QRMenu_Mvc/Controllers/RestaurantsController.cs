using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMenu_Mvc.Data;
using QRMenu_Mvc.Models;

namespace QRMenu_Mvc.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RestaurantsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Restaurant.Include(r => r.Brand).Include(r => r.States);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .Include(r => r.Brand)
                .Include(r => r.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BrandAdmin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,PostalCode,AddressDetail,DateTime,StateId,BrandId")] Restaurant restaurant)
        {

            AppUser applicationUser = new AppUser();
            Claim claim;

            _context.Restaurant.Add(restaurant);
            _context.SaveChanges();
            applicationUser.BrandId = restaurant.Id;
            applicationUser.Email = "abc@def.com";
            applicationUser.Name = "RestaurantAdmin";
            applicationUser.PhoneNumber = "1112223344";
            applicationUser.RegisterDate = DateTime.Today;
            applicationUser.StateId = 1;
            applicationUser.UserName = "RestAdmin" + restaurant.Id.ToString();
            _userManager.CreateAsync(applicationUser, "RestAdmin123!").Wait();
            _userManager.AddToRoleAsync(applicationUser, "RestaurantAdmin").Wait();
            claim = new Claim("RestaurantId", restaurant.Id.ToString());
            _userManager.AddClaimAsync(applicationUser, claim).Wait();
            return RedirectToAction("Index");

            //if (ModelState.IsValid)
            //{
            //    _context.Add(restaurant);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", restaurant.BrandId);
            //ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", restaurant.StateId);
            //return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", restaurant.BrandId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", restaurant.StateId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BrandAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,PostalCode,AddressDetail,DateTime,StateId,BrandId")] Restaurant restaurant)
        {
            if (User.HasClaim("BrandId", restaurant.Id.ToString()) == false)
            {
                return Unauthorized();
            }
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", restaurant.BrandId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", restaurant.StateId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .Include(r => r.Brand)
                .Include(r => r.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Restaurant == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Restaurant'  is null.");
            }
            Restaurant restaurant = _context.Restaurant!.Find(id)!;

            restaurant.StateId = 0;
            _context.Restaurant.Update(restaurant);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    

        private bool RestaurantExists(int id)
        {
          return (_context.Restaurant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
