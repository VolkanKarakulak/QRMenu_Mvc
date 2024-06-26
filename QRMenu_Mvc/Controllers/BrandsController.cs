﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMenu_Mvc.Data;
using QRMenu_Mvc.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QRMenu_Mvc.Controllers
{
   
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BrandsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Brands
        [Authorize(Roles = "Admin, BrandAdmin, RestaurantAdmin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Brand.Include(b => b.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Brands/Details/5
        [Authorize(Roles = "Admin, BrandAdmin, RestaurantAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brand == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand
                .Include(b => b.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name");
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,PostalCode,Address,Phone,EMail,RegisterDate,TaxNumber,WebbAddress,StateId")] Brand brand)
        {
            AppUser applicationUser = new AppUser();
            Claim claim;

            _context.Brand.Add(brand);
            _context.SaveChanges();
            applicationUser.BrandId = brand.Id;
            applicationUser.Email = "abc@def.com";
            applicationUser.Name = "Admin";
            applicationUser.PhoneNumber = "1112223344";
            applicationUser.RegisterDate = DateTime.Today;
            applicationUser.StateId = 1;
            applicationUser.UserName = "Admin" + brand.Id.ToString();
            _userManager.CreateAsync(applicationUser, "Admin123!").Wait();
            _userManager.AddToRoleAsync(applicationUser, "BrandAdmin").Wait();
            claim = new Claim("BrandId", brand.Id.ToString());
            _userManager.AddClaimAsync(applicationUser, claim).Wait();
            return RedirectToAction("Index");
        }

        // GET: Brands/Edit/5
        [Authorize(Roles = "Admin, BrandAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brand == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", brand.StateId);
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, BrandAdmin")]
        //[Authorize(Policy = "BrdAdmin")] // hem policy hem roles aynı ayna olmaz
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PostalCode,Address,Phone,EMail,RegisterDate,TaxNumber,WebbAddress,StateId")] Brand brand)
        {
            if (User.HasClaim("BrandId", brand.Id.ToString()) == false)
            {
                return Unauthorized();
            }
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", brand.StateId);
            return View(brand);
        }

        // GET: Brands/Delete/5
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || _context.Brand == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand
                .Include(b => b.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brand == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Brand'  is null.");
            }
            var brand = await _context.Brand.FindAsync(id);
            if (brand != null)
            {
                _context.Brand.Remove(brand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
          return (_context.Brand?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
