using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Models;

namespace SAcademy.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Header()
        {
            return View(await _db.Headers.ToListAsync());
        }


        public IActionResult CustomHeader()
        {
            return View(_db.Headers.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomHeader([Bind("Id,Background,Content,Button,Video,TopSize,LeftSize")] Header header)
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
                    await _db.AddAsync(customHeader);
                    await _db.SaveChangesAsync();
                }
                catch
                {
                     throw;
                }
                return RedirectToAction(nameof(CustomHeader));
            }

            return View(header);
        }



        public async Task<IActionResult> EditHeader(string id)
        {
            if (id == null || _db.Headers == null)
            {
                return NotFound();
            }

            var header = await _db.Headers.FindAsync(id);
            if (header == null)
            {
                return NotFound();
            }
            return PartialView("_EditHeaderPartialView", header);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHeader(string id, [Bind("Id,Background,Content,Button,Video,TopSize,LeftSize")] Header header)
        {
            if (id != header.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(header);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction(nameof(CustomHeader));
            }
            return View(header);
        }
    }
}
