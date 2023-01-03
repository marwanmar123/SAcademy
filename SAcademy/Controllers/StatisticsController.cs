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
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Statistics
        public async Task<IActionResult> Index()
        {
              return View(await _context.Statistics.ToListAsync());
        }

        // GET: Statistics/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.Statistics == null)
        //    {
        //        return NotFound();
        //    }

        //    var statistics = await _context.Statistics
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (statistics == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(statistics);
        //}

        // GET: Statistics/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,TitleColor,TitleSize")] Statistics statistics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statistics);
        }

        // GET: Statistics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics.FindAsync(id);
            if (statistics == null)
            {
                return NotFound();
            }
            return View(statistics);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,TitleColor,TitleSize")] Statistics statistics)
        {
            if (id != statistics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatisticsExists(statistics.Id))
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
            return View(statistics);
        }

        // GET: Statistics/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

        // POST: Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Statistics == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Statistics'  is null.");
            }
            var statistics = await _context.Statistics.FindAsync(id);
            if (statistics != null)
            {
                _context.Statistics.Remove(statistics);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatisticsExists(string id)
        {
          return _context.Statistics.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> isVisible(bool? visible, string? id)
        {

            var statistic = _context.Statistics.FirstOrDefault(a => a.Id == id);
            statistic.Visible = visible;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
