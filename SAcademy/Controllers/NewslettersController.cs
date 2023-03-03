    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
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

        [Authorize(Roles = "Admin")]
        // GET: Newsletters
        public async Task<IActionResult> Index()
        {
              return View(await _context.Newsletters.ToListAsync());
        }

        public IActionResult ExcelNews()
        {
            var inscrits = _context.Newsletters.ToList();
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");
            var currentRow = 1;
            var id = 0;

            worksheet.Row(currentRow).Height = 25.0;
            worksheet.Row(currentRow).Style.Font.Bold = true;
            worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Cell(currentRow, 1).Value = "Id";

            worksheet.Cell(currentRow, 2).Value = "Nom";
            worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 3).Value = "Prenom";
            worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 5).Value = "Email";
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 4).Value = "Phone";
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 6).Value = "Entreprise";
            worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            worksheet.Cell(currentRow, 7).Value = "Ville";
            worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            foreach (var inscr in inscrits)
            {
                currentRow++;
                id++;

                worksheet.Row(currentRow).Height = 20.0;
                worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(currentRow, 1).Value = id;
                worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 2).Value = inscr.Nom;
                worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 3).Value = inscr.Prenom;
                worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 5).Value = inscr.Email;
                worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 4).Value = inscr.Phone;
                worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 6).Value = inscr.Region;
                worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 7).Value = inscr.Ville;
                worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                worksheet.Columns().AdjustToContents();
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NewsLetterSimplonAcademy.xlsx");
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
        [Authorize]
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

        [Authorize(Roles = "Admin")]
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
