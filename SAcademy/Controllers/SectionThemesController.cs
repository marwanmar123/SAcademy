 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Models;

namespace SAcademy.Controllers
{
    public class SectionThemesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionThemesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SectionThemes
        public async Task<IActionResult> Index()
        {
              return View(await _context.SectionTheme.ToListAsync());
        }

        // GET: SectionThemes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SectionTheme == null)
            {
                return NotFound();
            }

            var sectionTheme = await _context.SectionTheme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectionTheme == null)
            {
                return NotFound();
            }

            return View(sectionTheme);
        }

        // GET: SectionThemes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,TitleColor,TitleSize,Content,Visible")] SectionTheme sectionTheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectionTheme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sectionTheme);
        }

        // GET: SectionThemes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SectionTheme == null)
            {
                return NotFound();
            }

            var sectionTheme = await _context.SectionTheme.FindAsync(id);
            if (sectionTheme == null)
            {
                return NotFound();
            }
            return View(sectionTheme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,TitleColor,TitleSize,Content,Visible")] SectionTheme sectionTheme)
        {
            if (id != sectionTheme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectionTheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionThemeExists(sectionTheme.Id))
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
            return View(sectionTheme);
        }

        // GET: SectionThemes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SectionTheme == null)
            {
                return NotFound();
            }

            var sectionTheme = await _context.SectionTheme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectionTheme == null)
            {
                return NotFound();
            }

            return View(sectionTheme);
        }

        // POST: SectionThemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SectionTheme == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SectionTheme'  is null.");
            }
            var sectionTheme = await _context.SectionTheme.FindAsync(id);
            if (sectionTheme != null)
            {
                _context.SectionTheme.Remove(sectionTheme);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionThemeExists(string id)
        {
          return _context.SectionTheme.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> isVisible(bool? visible, string? id)
        {

            var sThem = _context.SectionTheme.FirstOrDefault(a => a.Id == id);
            sThem.Visible = visible;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
