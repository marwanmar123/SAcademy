﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SAcademy.Data;
using SAcademy.Models;

namespace SAcademy.Controllers
{
    public class ThematicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThematicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Thematics
        public async Task<IActionResult> Index()
        {
              return View(await _context.Thematics.ToListAsync());
        }

        // GET: Thematics/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Thematics == null)
            {
                return NotFound();
            }

            var thematic = await _context.Thematics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thematic == null)
            {
                return NotFound();
            }

            return View(thematic);
        }

        // GET: Thematics/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ColorTitle,Background,TypeId")] Thematic thematic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thematic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name", thematic.TypeId);
            return View(thematic);
        }

        // GET: Thematics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Thematics == null)
            {
                return NotFound();
            }

            var thematic = await _context.Thematics.FindAsync(id);
            if (thematic == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name", thematic.TypeId);
            return View(thematic);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,ColorTitle,Background,TypeId")] Thematic thematic)
        {
            if (id != thematic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thematic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThematicExists(thematic.Id))
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
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name", thematic.TypeId);
            return View(thematic);
        }

        // GET: Thematics/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Thematics == null)
            {
                return NotFound();
            }

            var thematic = await _context.Thematics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thematic == null)
            {
                return NotFound();
            }

            return View(thematic);
        }

        // POST: Thematics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Thematics == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Thematics'  is null.");
            }
            var thematic = await _context.Thematics.FindAsync(id);
            if (thematic != null)
            {
                _context.Thematics.Remove(thematic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThematicExists(string id)
        {
          return _context.Thematics.Any(e => e.Id == id);
        }
    }
}
