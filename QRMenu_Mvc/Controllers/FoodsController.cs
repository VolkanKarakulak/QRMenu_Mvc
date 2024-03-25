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
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public FoodsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Food.Include(f => f.Category).Include(f => f.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .Include(f => f.Category)
                .Include(f => f.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RestaurantAdmin")]
        public async Task<IActionResult> Create(int id,[Bind("Id,Name,Price,Description,ImageFileName,CategoryId,StateId")] Food food, IFormFile ImageFileName)
        {
            if (User.HasClaim("RestaurantId", id.ToString()) || User.IsInRole("RestaurantAdmin"))
            {
                return Unauthorized();
            }

                if (ModelState.IsValid)
                {
                string uploadedFileName = ImageFileName.FileName;

                string uploadedFileExtension = System.IO.Path.GetExtension(uploadedFileName);

                string fileNameToUse = Guid.NewGuid().ToString() + uploadedFileExtension;

                string savePath = Path.Combine(_environment.WebRootPath, "img/", fileNameToUse);


                FileStream stream = new FileStream(savePath, FileMode.Create);

                ImageFileName.CopyTo(stream);

                food.ImageFileName = fileNameToUse;
                // Veritabanına kaydetme işlemi
                _context.Add(food);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", food.CategoryId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", food.StateId);
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", food.CategoryId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", food.StateId);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Price,Description,ImageFileName,CategoryId,StateId")] Food food, IFormFile ImageFileName)
        {

            if (ModelState.IsValid)
            {
                _context.Update(food);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }


            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", food.CategoryId);
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", food.StateId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .Include(f => f.Category)
                .Include(f => f.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Food == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Food'  is null.");
            }
            Food food = _context.Food!.Find(id)!;

            food.StateId = 0;
            _context.Food.Update(food);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
          return (_context.Food?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
