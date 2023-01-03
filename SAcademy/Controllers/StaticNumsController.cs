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
    public class StaticNumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaticNumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StaticNums
        public async Task<IActionResult> Index()
        {
              return View(await _context.StaticNums.ToListAsync());
        }

        // GET: StaticNums/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.StaticNums == null)
        //    {
        //        return NotFound();
        //    }

        //    var staticNum = await _context.StaticNums
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (staticNum == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(staticNum);
        //}

        // GET: StaticNums/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Description")] StaticNum staticNum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staticNum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staticNum);
        }

        // GET: StaticNums/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.StaticNums == null)
            {
                return NotFound();
            }

            var staticNum = await _context.StaticNums.FindAsync(id);
            if (staticNum == null)
            {
                return NotFound();
            }
            return View(staticNum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Number,Description")] StaticNum staticNum)
        {
            if (id != staticNum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staticNum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaticNumExists(staticNum.Id))
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
            return View(staticNum);
        }

        // GET: StaticNums/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.StaticNums == null)
            {
                return NotFound();
            }

            var staticNum = await _context.StaticNums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staticNum == null)
            {
                return NotFound();
            }

            return View(staticNum);
        }

        // POST: StaticNums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.StaticNums == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StaticNums'  is null.");
            }
            var staticNum = await _context.StaticNums.FindAsync(id);
            if (staticNum != null)
            {
                _context.StaticNums.Remove(staticNum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaticNumExists(string id)
        {
          return _context.StaticNums.Any(e => e.Id == id);
        }
    }
}
