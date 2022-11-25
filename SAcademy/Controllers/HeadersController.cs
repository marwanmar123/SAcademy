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
    public class HeadersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HeadersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Headers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Headers.ToListAsync());
        }

        // GET: Headers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Headers == null)
            {
                return NotFound();
            }

            var header = await _context.Headers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (header == null)
            {
                return NotFound();
            }

            return View(header);
        }

        // GET: Headers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Headers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Background,Content,Button,Video,TopSize,LeftSize")] Header header)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customHeader = new Header
                    {
                        Content = header.Content,
                        Button = header.Button,
                        Video = header.Video,
                        TopSize = header.TopSize,
                        LeftSize = header.LeftSize,
                    };

                    if (Request.Form.Files.Count > 0)
                    {
                        IFormFile file = Request.Form.Files.FirstOrDefault();
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            customHeader.Background = dataStream.ToArray();
                        }
                    }
                    await _context.AddAsync(customHeader);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(header);
        }

        // GET: Headers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Headers == null)
            {
                return NotFound();
            }

            var header = await _context.Headers.FindAsync(id);
            if (header == null)
            {
                return NotFound();
            }
            return PartialView("_EditHeaderPartialView",header);
        }

        // POST: Headers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Background,Content,Button,Video,TopSize,LeftSize")] Header header)
        {
            if (id != header.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(header);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeaderExists(header.Id))
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
            return View(header);
        }

        // GET: Headers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Headers == null)
            {
                return NotFound();
            }

            var header = await _context.Headers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (header == null)
            {
                return NotFound();
            }

            return View(header);
        }

        // POST: Headers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Headers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Headers'  is null.");
            }
            var header = await _context.Headers.FindAsync(id);
            if (header != null)
            {
                _context.Headers.Remove(header);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeaderExists(string id)
        {
          return _context.Headers.Any(e => e.Id == id);
        }
    }
}
