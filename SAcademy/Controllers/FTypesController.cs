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
    public class FTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.FTypes.ToListAsync());
        }

        public async Task<IActionResult> GetTypeAPI()
        {
            return Ok(await _context.FTypes.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Color,BgColor")] FType fType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fType);
                await _context.SaveChangesAsync();
                return RedirectToAction("FormationPanel", "FormationPages");
            }
            return View(fType);
        }

        // GET: FTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FTypes == null)
            {
                return NotFound();
            }

            var fType = await _context.FTypes.FindAsync(id);
            if (fType == null)
            {
                return NotFound();
            }
            return View(fType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Color,BgColor")] FType fType)
        {
            if (id != fType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FTypeExists(fType.Id))
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
            return View(fType);
        }

        // GET: FTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.FTypes == null)
            {
                return NotFound();
            }

            var fType = await _context.FTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fType == null)
            {
                return NotFound();
            }

            return View(fType);
        }

        // POST: FTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FTypes'  is null.");
            }
            var fType = await _context.FTypes.Include(ft => ft.Formations).FirstOrDefaultAsync(m => m.Id == id);
            if (fType != null)
            {
                _context.FTypes.Remove(fType);
            }
            foreach (var f in fType.Formations)
            {
                _context.Remove(f);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("FormationPanel", "FormationPages");
        }

        private bool FTypeExists(string id)
        {
          return _context.FTypes.Any(e => e.Id == id);
        }
    }
}
