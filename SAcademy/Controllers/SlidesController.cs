using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Ocsp;
using SAcademy.Data;
using SAcademy.Models;

namespace SAcademy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SlidesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlidesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Slides
        public async Task<IActionResult> Index()
        {
              return View(await _context.Slides.Include(x => x.Images).ToListAsync());
        }


        // GET: Slides/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slide slide, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var addSlide = new Slide
                {
                    Title = slide.Title,
                    Content = slide.Content
                };

                _context.Add(slide);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var fileModel = new Image
                    {
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        SlideId = slide.Id
                    };
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        fileModel.Data = dataStream.ToArray();
                    }
                    await _context.AddAsync(fileModel);
                }
            }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Slides/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Slides == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides.Include(x => x.Images).FirstOrDefaultAsync(x=> x.Id == id);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Slide slide, List<IFormFile> files)
        {

            _context.Update(slide);

            var slideId = _context.Slides.Include(x => x.Images).FirstOrDefault(a => a.Id == id);
            foreach (var m in slideId.Images)
            {
                _context.Remove(m);
            }
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var fileModel = new Image
                {
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName,
                    SlideId = slide.Id
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                await _context.AddAsync(fileModel);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Slides/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Slides == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // POST: Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Slides == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slides'  is null.");
            }
            var slide = await _context.Slides.FindAsync(id);
            if (slide != null)
            {
                var slideId = _context.Slides.Include(x => x.Images).FirstOrDefault(a => a.Id == id);
                foreach (var m in slideId.Images)
                {
                    _context.Remove(m);
                }
                _context.Remove(slideId);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideExists(string id)
        {
          return _context.Slides.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> isVisible(bool? visible, string? id)
        {

            var slide = _context.Slides.FirstOrDefault(a => a.Id == id);
            slide.Visible = visible;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
