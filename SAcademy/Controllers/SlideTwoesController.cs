using System;
using System.Collections.Generic;
using System.Data;
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
    public class SlideTwoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlideTwoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SlideTwoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.SlideTwos.Include(x => x.Images).ToListAsync());
        }

        //// GET: SlideTwoes/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.SlideTwos == null)
        //    {
        //        return NotFound();
        //    }

        //    var slideTwo = await _context.SlideTwos
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (slideTwo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(slideTwo);
        //}

        // GET: SlideTwoes/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlideTwo slideTwo, List<IFormFile> files)
        {
            //if (ModelState.IsValid)
            //{
                var addSlide = new SlideTwo
                {
                    Title = slideTwo.Title,
                    Content = slideTwo.Content,
                    ContentTwo = slideTwo.ContentTwo,
                    FontFamily = slideTwo.FontFamily,
                    TitleColor = slideTwo.TitleColor,
                    TitleSize = slideTwo.TitleSize
                };

                await _context.AddAsync(slideTwo);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var fileModel = new Image
                    {
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        SlideTwoId = slideTwo.Id
                    };
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        fileModel.Data = dataStream.ToArray();
                    }
                    await _context.AddAsync(fileModel);
                }
            //}

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: SlideTwoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SlideTwos.Include(x => x.Images) == null)
            {
                return NotFound();
            }

            var slideTwo = await _context.SlideTwos.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
            if (slideTwo == null)
            {
                return NotFound();
            }
            return View(slideTwo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SlideTwo slideTwo, List<IFormFile> files)
        {
            _context.Update(slideTwo);

            var slideTwoId = await _context.SlideTwos.Include(x => x.Images).FirstOrDefaultAsync(a => a.Id == id);
            foreach (var img in slideTwoId.Images)
            {
                _context.Remove(img);
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
                    SlideTwoId = slideTwo.Id
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

        // GET: SlideTwoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SlideTwos == null)
            {
                return NotFound();
            }

            var slideTwo = await _context.SlideTwos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slideTwo == null)
            {
                return NotFound();
            }

            return View(slideTwo);
        }

        // POST: SlideTwoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SlideTwos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SlideTwos'  is null.");
            }
            var slideTwo = await _context.SlideTwos.FindAsync(id);
            if (slideTwo != null)
            {
                _context.SlideTwos.Remove(slideTwo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideTwoExists(string id)
        {
          return _context.SlideTwos.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> isVisible(bool? visible, string? id)
        {

            var slide = _context.SlideTwos.FirstOrDefault(a => a.Id == id);
            slide.Visible = visible;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
