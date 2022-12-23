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
    public class ModesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Modes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Modes.ToListAsync());
        }

        public async Task<IActionResult> GetModeAPI()
        {
            return Ok(await _context.Modes.ToListAsync());
        }

        // GET: Modes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Modes == null)
            {
                return NotFound();
            }

            var mode = await _context.Modes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mode == null)
            {
                return NotFound();
            }

            return View(mode);
        }

        // GET: Modes/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Mode mode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mode);
                await _context.SaveChangesAsync();
                return RedirectToAction("FormationPanel", "FormationPages");
            }
            return View(mode);
        }

        // GET: Modes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Modes == null)
            {
                return NotFound();
            }

            var mode = await _context.Modes.FindAsync(id);
            if (mode == null)
            {
                return NotFound();
            }
            return View(mode);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] Mode mode)
        {
            if (id != mode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeExists(mode.Id))
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
            return View(mode);
        }

        // GET: Modes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Modes == null)
            {
                return NotFound();
            }

            var mode = await _context.Modes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mode == null)
            {
                return NotFound();
            }

            return View(mode);
        }

        // POST: Modes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Modes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Modes'  is null.");
            }
            var mode = await _context.Modes.Include(m => m.Formations).FirstOrDefaultAsync(m => m.Id == id);
            if (mode != null)
            {
                _context.Modes.Remove(mode);
            }
            foreach (var f in mode.Formations)
            {
                _context.Remove(f);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("FormationPanel", "FormationPages");
        }

        private bool ModeExists(string id)
        {
          return _context.Modes.Any(e => e.Id == id);
        }
    }
}
