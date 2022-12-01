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
    public class InscriptionPagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InscriptionPagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InscriptionPages
        public async Task<IActionResult> Index()
        {
              return View(await _context.InscriptionPages.ToListAsync());
        }

        // GET: InscriptionPages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.InscriptionPages == null)
            {
                return NotFound();
            }

            var inscriptionPage = await _context.InscriptionPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inscriptionPage == null)
            {
                return NotFound();
            }

            return View(inscriptionPage);
        }

        // GET: InscriptionPages/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContentOne,ContentBgColor,ContentHeight,ContentTwo")] InscriptionPage inscriptionPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscriptionPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inscriptionPage);
        }

        // GET: InscriptionPages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.InscriptionPages == null)
            {
                return NotFound();
            }

            var inscriptionPage = await _context.InscriptionPages.FindAsync(id);
            if (inscriptionPage == null)
            {
                return NotFound();
            }
            return PartialView("_EditInscriptionPagePartialView", inscriptionPage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ContentOne,ContentBgColor,ContentHeight,ContentTwo")] InscriptionPage inscriptionPage)
        {
            if (id != inscriptionPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscriptionPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscriptionPageExists(inscriptionPage.Id))
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
            return View(inscriptionPage);
        }

        // GET: InscriptionPages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.InscriptionPages == null)
            {
                return NotFound();
            }

            var inscriptionPage = await _context.InscriptionPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inscriptionPage == null)
            {
                return NotFound();
            }

            return View(inscriptionPage);
        }

        // POST: InscriptionPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.InscriptionPages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InscriptionPages'  is null.");
            }
            var inscriptionPage = await _context.InscriptionPages.FindAsync(id);
            if (inscriptionPage != null)
            {
                _context.InscriptionPages.Remove(inscriptionPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscriptionPageExists(string id)
        {
          return _context.InscriptionPages.Any(e => e.Id == id);
        }
    }
}
