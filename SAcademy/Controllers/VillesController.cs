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
    
    public class VillesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: Villes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Villes.ToListAsync());
        }

        public async Task<IActionResult> GetVilleAPI()
        {
            var villes = await _context.Villes.Include(x=>x.Formations).ToListAsync();
            return Ok(villes);
        }
        [Authorize(Roles = "Admin")]
        // GET: Villes/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ville);
                await _context.SaveChangesAsync();
                return RedirectToAction("FormationPanel", "FormationPages");
            }
            return View(ville);
        }
        [Authorize(Roles = "Admin")]
        // GET: Villes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Villes == null)
            {
                return NotFound();
            }

            var ville = await _context.Villes.FindAsync(id);
            if (ville == null)
            {
                return NotFound();
            }
            return View(ville);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] Ville ville)
        {
            if (id != ville.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ville);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VilleExists(ville.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FormationPanel", "FormationPages");
            }
            return View(ville);
        }
        [Authorize(Roles = "Admin")]
        // GET: Villes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Villes == null)
            {
                return NotFound();
            }

            var ville = await _context.Villes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ville == null)
            {
                return NotFound();
            }

            return View(ville);
        }

        // POST: Villes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Villes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Villes'  is null.");
            }
            var ville = await _context.Villes.Include(v => v.Formations).ThenInclude(ft => ft.Registration).FirstOrDefaultAsync(m => m.Id == id);
            if (ville != null)
            {
                _context.Villes.Remove(ville);
            }
            foreach (var f in ville.Formations)
            {
                _context.Remove(f);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("FormationPanel", "FormationPages");
        }

        private bool VilleExists(string id)
        {
          return _context.Villes.Any(e => e.Id == id);
        }
    }
}
