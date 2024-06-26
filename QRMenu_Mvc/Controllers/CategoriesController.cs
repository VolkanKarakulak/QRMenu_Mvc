﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMenu_Mvc.Data;
using QRMenu_Mvc.Models;

namespace QRMenu_Mvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Category.Include(c => c.Restaurant).Include(c => c.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.Restaurant)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Set<Restaurant>(), "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StateId,RestaurantId")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Set<Restaurant>(), "Id", "Name", category.RestaurantId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", category.StateId);
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "BrandAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Set<Restaurant>(), "Id", "Name", category.RestaurantId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", category.StateId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BrandAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StateId,RestaurantId")] Category category)
        {
           
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewData["RestaurantId"] = new SelectList(_context.Set<Restaurant>(), "Id", "Name", category.RestaurantId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", category.StateId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.Restaurant)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BrandAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.HasClaim("RestautantId", id.ToString()) || User.IsInRole("BrandAdmin"))
            {
                var category = _context.Category!.FindAsync(id).Result;
                category!.StateId = 0;
                _context.Category.Update(category);

                var foods = _context.Food.Where(f => f.CategoryId == category.Id);
                foreach (Food food in foods)
                {
                    food.StateId = 0;
                    _context.Food.Update(food);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
