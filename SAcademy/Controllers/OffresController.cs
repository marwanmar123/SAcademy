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

namespace SAcademy.Controllers
{
    [Authorize]
    public class OffresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Offres
        public async Task<IActionResult> Index()
        {
              return View(await _context.Offres.ToListAsync());
        }


        // GET: Offres/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,TitleColor,TitleSize,Content")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offre);
        }

        // GET: Offres/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Offres == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres.FindAsync(id);
            if (offre == null)
            {
                return NotFound();
            }
            return View(offre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,TitleColor,TitleSize,Content")] Offre offre)
        {
            if (id != offre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffreExists(offre.Id))
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
            return View(offre);
        }

        // GET: Offres/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Offres == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }

        // POST: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Offres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Offre'  is null.");
            }
            var offre = await _context.Offres.FindAsync(id);
            if (offre != null)
            {
                _context.Offres.Remove(offre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffreExists(string id)
        {
          return _context.Offres.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> isVisible(bool? visible, string? id)
        {

            var offre = _context.Offres.FirstOrDefault(a => a.Id == id);
            offre.Visible = visible;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
