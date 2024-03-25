//using Microsoft.AspNet.Identity;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMenu_Mvc.Data;
using QRMenu_Mvc.Models;

//[Authorize]
public class UsersController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public UsersController(SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
    }
    // GET: Users
    public async Task<IActionResult> Index()
    {
        var users = await _signInManager.UserManager.Users.Include(u => u.State).Include(b => b.Brand).ToListAsync();
        return View(users);
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _signInManager.UserManager.Users
                 .Include(u => u.Brand)
                 .Include(u => u.State).Include(u => u.Restaurants)
                 .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // GET: Users/Create
    public IActionResult Create()
    {
        ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name");
        ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "Id", "Name");
        return View();
    }

    // POST: Users/Create
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AppUser appUser, string password)
    {
        await _signInManager.UserManager.CreateAsync(appUser, password);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
    {

        if (id == null)
        {
            return NotFound();
        }

        var user = await _signInManager.UserManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Id", user.BrandId);
        ViewData["StateId"] = new SelectList(_context.State, "Id", "Name", user.StateId);

        return View(user);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(string id,[Bind("Id,Name,UserName,SurName,Address,Email,RegisterDate,PostalCode,PhoneNumber,StateId,BrandId")] AppUser appUser)
    {
      
           
                var user = await _signInManager.UserManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return NotFound();
                }

                // Kullanıcı verilerini güncelle
                user.UserName = appUser.UserName;
                user.Name = appUser.Name;
                user.SurName = appUser.SurName;  
                user.Email = appUser.Email; 
                user.PostalCode = appUser.PostalCode;
                user.PhoneNumber = appUser.PhoneNumber;
                user.Address = appUser.Address;
                user.StateId = appUser.StateId;
                user.BrandId = appUser.BrandId;

                _context.Update(user);
                await _context.SaveChangesAsync();

        // ModelState geçerli değilse, tekrar view'e orijinal veriyi yükleyin
        ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", appUser.BrandId);
        ViewData["StateId"] = new SelectList(_context.State, "Id", "Name", appUser.StateId);
        return RedirectToAction("Index");
    }


    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //[Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {

        AppUser applicationUser = _signInManager.UserManager.FindByIdAsync(id).Result;

        if (applicationUser == null)
        {
            return NotFound();
        }
        applicationUser.StateId = 0;
        await _signInManager.UserManager.UpdateAsync(applicationUser);
        return RedirectToAction("Index");
    }
    public ViewResult LogIn()
    {
            return View();
    }

    [HttpPost] // sunucuya bir şey göndermek için
    public ActionResult LogIn(string userName, string passWord)
    {
        Microsoft.AspNetCore.Identity.SignInResult signInResult;
        AppUser appUser = _signInManager.UserManager.FindByNameAsync(userName).Result;

        if (appUser == null)
        {
            return RedirectToAction("LogIn");
        }

        signInResult = _signInManager.PasswordSignInAsync(appUser, passWord, false, false).Result;

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Users");
    }
}

