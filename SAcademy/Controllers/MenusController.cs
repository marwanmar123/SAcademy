using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Models;
using SAcademy.ViewModel;

namespace SAcademy.Controllers
{
    [Authorize]
    public class MenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var Nav = new NavViewModel()
            {
                Menu = await _context.Menus.ToListAsync(),
                Home = await _context.Homes.ToListAsync(),
            };
            return View(Nav);
        }


        // GET: Menus/Create
        public IActionResult Create()
        {
            Menu menu = new Menu();
            return PartialView("_CreateMenuPartialView",menu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TitleMenu,Link,Color,ColorFooter,FontFamily,Size,SizeFooter")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return PartialView("_EditMenuPartialView",menu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,TitleMenu,Link,Color,ColorFooter,FontFamily,Size,SizeFooter")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteMenuPartialView",menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(string id)
        {
          return _context.Menus.Any(e => e.Id == id);
        }


        public IActionResult CreateHomeNav()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHomeNav([Bind("Id,BgNav,Logo,LogoSize")] Home home)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        home.Logo = dataStream.ToArray();
                    }
                }
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }


        public async Task<IActionResult> EditHomeNav(string id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHomeNav(string id, [Bind("Id,BgNav,Logo,LogoSize")] Home home)
        {
            if (id != home.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        IFormFile file = Request.Form.Files.FirstOrDefault();
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            home.Logo = dataStream.ToArray();
                        }
                    }
                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }
    }
}

