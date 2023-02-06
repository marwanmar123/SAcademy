using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Data.Migrations;
using SAcademy.Models;

namespace SAcademy.Controllers
{
    [Authorize(Roles = "Admin")]
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
              return View(await _context.Headers.Include(x=>x.Images).ToListAsync());
        }

        
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Header header, List<IFormFile> files)
        {
            //if (ModelState.IsValid)
            //{
            var addHeader = new Header
            {
                Content = header.Content,
                ContentTwo = header.ContentTwo,
                ContentThree = header.ContentThree,
                BgContent= header.BgContent,
                BgContentTwo= header.BgContentTwo,
                Button= header.Button,
                ButtonTwo= header.ButtonTwo,
                ButtonThree= header.ButtonThree,
                ButtonBgColor = header.ButtonBgColor,
                BVColor = header.BVColor,
                BVLeftSize = header.BVLeftSize,
                BVSize = header.BVSize,
                BVTopSize= header.BVTopSize,
                HeightSection=header.HeightSection,
                Images = header.Images,
                Video= header.Video,
                
            };

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    addHeader.Background = dataStream.ToArray();
                }
            }
            //if(header.BackgroundTwo != null)
            //{
            //    using (var dataStream = new MemoryStream())
            //    {
            //        addHeader.Background = dataStream.ToArray();
            //    }
            //}
            await _context.AddAsync(addHeader);


            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var fileModel = new Image
                {
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName,
                    HeaderId = header.Id
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

        
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Headers.Include(x => x.Images) == null)
            {
                return NotFound();
            }

            var header = await _context.Headers.Include(x => x.Images).FirstOrDefaultAsync(x =>x.Id == id);
            //if (Request.Form.Files.Count > 0)
            //{
            //    IFormFile file = Request.Form.Files.FirstOrDefault();
            //    using (var dataStream = new MemoryStream())
            //    {
            //        await file.CopyToAsync(dataStream);
            //        header.Background = dataStream.ToArray();
            //        //_context.Update(header);
            //    }
            //}
            if (header == null)
            {
                return NotFound();
            }
            return PartialView("_EditHeaderPartialView",header);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Header header, List<IFormFile> files)
        {
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    header.Background = dataStream.ToArray();
                    header.BackgroundTwo = dataStream.ToArray();
                    //_context.Update(header);
                }
            }
            _context.Update(header);

            var headerId = await _context.Headers.Include(x => x.Images).FirstOrDefaultAsync(a => a.Id == id);
            foreach (var img in headerId.Images)
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
                    HeaderId = header.Id
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
