using System;
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
    [Authorize(Roles = "Admin")]
    public class MainCompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainCompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MainCompanies
        public async Task<IActionResult> Index()
        {
              return _context.MainCompany != null ? 
                          View(await _context.MainCompany.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MainCompany'  is null.");
        }

        // GET: MainCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MainCompany == null)
            {
                return NotFound();
            }

            var mainCompany = await _context.MainCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainCompany == null)
            {
                return NotFound();
            }

            return View(mainCompany);
        }

        // GET: MainCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PostalCode,Address,Phone,EMail,RegisterDate,TaxNumber,WebbAddress")] MainCompany mainCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainCompany);
        }

        // GET: MainCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MainCompany == null)
            {
                return NotFound();
            }

            var mainCompany = await _context.MainCompany.FindAsync(id);
            if (mainCompany == null)
            {
                return NotFound();
            }
            return View(mainCompany);
        }

        // POST: MainCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PostalCode,Address,Phone,EMail,RegisterDate,TaxNumber,WebbAddress")] MainCompany mainCompany)
        {
            if (id != mainCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainCompanyExists(mainCompany.Id))
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
            return View(mainCompany);
        }

        // GET: MainCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MainCompany == null)
            {
                return NotFound();
            }

            var mainCompany = await _context.MainCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainCompany == null)
            {
                return NotFound();
            }

            return View(mainCompany);
        }

        // POST: MainCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MainCompany == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MainCompany'  is null.");
            }
            var mainCompany = await _context.MainCompany.FindAsync(id);
            if (mainCompany != null)
            {
                _context.MainCompany.Remove(mainCompany);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainCompanyExists(int id)
        {
          return (_context.MainCompany?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
