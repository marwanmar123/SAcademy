﻿using System;
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
    public class FormationPagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormationPagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FormationPages
        public async Task<IActionResult> Index()
        {
              return View(await _context.FormationPages.ToListAsync());
        }

        //// GET: FormationPages/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.FormationPages == null)
        //    {
        //        return NotFound();
        //    }

        //    var formationPage = await _context.FormationPages
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (formationPage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(formationPage);
        //}

        // GET: FormationPages/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,ContentBgColor,ContentHeight,DescriptionFilter")] FormationPage formationPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formationPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formationPage);
        }

        // GET: FormationPages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FormationPages == null)
            {
                return NotFound();
            }

            var formationPage = await _context.FormationPages.FindAsync(id);
            if (formationPage == null)
            {
                return NotFound();
            }
            return PartialView("_EditFomationPagesPartialView", formationPage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content,ContentBgColor,ContentHeight,DescriptionFilter")] FormationPage formationPage)
        {
            if (id != formationPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formationPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationPageExists(formationPage.Id))
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
            return View(formationPage);
        }

        // GET: FormationPages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.FormationPages == null)
            {
                return NotFound();
            }

            var formationPage = await _context.FormationPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formationPage == null)
            {
                return NotFound();
            }

            return View(formationPage);
        }

        // POST: FormationPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FormationPages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FormationPages'  is null.");
            }
            var formationPage = await _context.FormationPages.FindAsync(id);
            if (formationPage != null)
            {
                _context.FormationPages.Remove(formationPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationPageExists(string id)
        {
          return _context.FormationPages.Any(e => e.Id == id);
        }
    }
}