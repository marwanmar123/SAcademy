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
    [Authorize(Roles = "Admin")]
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sections
        public async Task<IActionResult> Index()
        {
              return View(await _context.Sections.ToListAsync());
        }


        // GET: Sections/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,TitleColor,TitleSize,Content,Video,image,VideoWidth,VideoHeight,Visible")] Section section)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        section.image = dataStream.ToArray();
                    }
                }
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }

        // GET: Sections/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,TitleColor,TitleSize,Content,Video,image,VideoWidth,VideoHeight,Visible")] Section section)
        {
            if (id != section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        IFormFile file = Request.Form.Files.FirstOrDefault();
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            section.image = dataStream.ToArray();
                        }
                    }
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Id))
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
            return View(section);
        }

        // GET: Sections/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Sections == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sections'  is null.");
            }
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                _context.Sections.Remove(section);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(string id)
        {
          return _context.Sections.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> isVisible(bool? visible, string? id)
        {

            var section = _context.Sections.FirstOrDefault(a => a.Id == id);
            section.Visible = visible;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
