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
    public class NewslettersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewslettersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Newsletters
        public async Task<IActionResult> Index()
        {
              return View(await _context.Newsletters.ToListAsync());
        }

        // GET: Newsletters/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.Newsletters == null)
        //    {
        //        return NotFound();
        //    }

        //    var newsletter = await _context.Newsletters
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (newsletter == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(newsletter);
        //}

        // GET: Newsletters/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Phone,Region,Ville")] Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsletter);
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "home");
            }
            return View(newsletter);
        }

        // GET: Newsletters/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _context.Newsletters == null)
        //    {
        //        return NotFound();
        //    }

        //    var newsletter = await _context.Newsletters.FindAsync(id);
        //    if (newsletter == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(newsletter);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,Nom,Prenom,Email,Phone,Region,Ville")] Newsletter newsletter)
        //{
        //    if (id != newsletter.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(newsletter);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!NewsletterExists(newsletter.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(newsletter);
        //}

        [Authorize]
        // GET: Newsletters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Newsletters == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        // POST: Newsletters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Newsletters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Newsletters'  is null.");
            }
            var newsletter = await _context.Newsletters.FindAsync(id);
            if (newsletter != null)
            {
                _context.Newsletters.Remove(newsletter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsletterExists(string id)
        {
          return _context.Newsletters.Any(e => e.Id == id);
        }
    }
}
