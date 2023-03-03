using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SAcademy.Data;
using SAcademy.Models;
using SAcademy.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SAcademy.Controllers
{
    public class FormationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public FormationsController(
            ApplicationDbContext context, 
            IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }


        // GET: Formations
        public async Task<IActionResult> Index()
        {
            var formation = await _context.Formations.AsNoTracking().Include(f => f.Thematic).Include(f => f.Mode).Include(f => f.Ville).Include(f => f.Type).ToListAsync();
            return View();
        }



        public async Task<IActionResult> Filter([FromQuery] string villeId, string themId, string modeId)
        {
            // Console.WriteLine("VV ", villeId.Split(','));
            // Console.WriteLine("TT ", typeId.Split(','));
            // Console.WriteLine("MM ", modeId.Split(','));

            List<Formation> formations = new List<Formation>();
            var query = _context.Formations.Include(f => f.Ville).Include(f => f.Mode).Include(f => f.Thematic).AsQueryable();
            if (!string.IsNullOrEmpty(villeId) || !string.IsNullOrEmpty(themId) || !string.IsNullOrEmpty(modeId))
            {

                string[] villes = villeId != null ? villeId.Split(',') : new string[] { };
                string[] themes = themId != null ? themId.Split(',') : new string[] { };
                string[] modes = modeId != null ? modeId.Split(',') : new string[] { };
                var select = query.Where(f => villes.Contains(f.VilleId) || themes.Contains(f.ThematicId) || modes.Contains(f.ModeId));

                formations = await select.ToListAsync();

            }
            else
            {
                formations = await query.ToListAsync();
            }
            return Ok(formations);
        }

        // GET: FormationsAPI
        public async Task<IActionResult> GetFormationTrue(string? typeId)
        {
            var fomramtionT = _context.Formations.AsNoTracking().Include(f => f.Mode).Include(f => f.Ville).Include(f => f.Thematic).Where(f => f.Status == true);
            return Ok(await fomramtionT.ToListAsync());
        }
        public async Task<IActionResult> GetFormationFalse(string? typeId)
        {
            var fomramtionF = _context.Formations.AsNoTracking().Include(f => f.Mode).Include(f => f.Ville).Include(f => f.Thematic).Where(f => f.Status == false);
            return Ok(await fomramtionF.ToListAsync());
        }

        // GET: FormationsByFilter
        public async Task<IActionResult> GetFormationsByFilter(string? ThemeId)
        {
            var formations = await _context.Formations.Include(f => f.Type).Include(f => f.Thematic).Where(f => f.ThematicId == ThemeId).ToListAsync();
            return Ok(formations);
        }
        [Authorize]
        // GET: Formations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }


            var formation = await _context.Formations
                .Include(f => f.Mode)
                .Include(f => f.Type)
                .Include(f => f.Ville)
                .Include(f => f.Thematic)
                .Include(f => f.Registration)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            TempData["title"] = formation.Title;
            return View(formation);
        }

        [Authorize(Roles = "Admin")]
        // GET: Formations/Create
        public IActionResult Create()
        {
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name");
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "Name");
            ViewData["ThematicId"] = new SelectList(_context.Thematics, "Id", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Duration,StartDay,EndDay,StartTime,EndTime,Certificate,Presentation,Skills,Status,ModeId,VilleId,TypeId,ThematicId,OffreFColor,OffreFSize,OffreFBgColor,OffreFBgColorButton")] Formation formation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formation);
                await _context.SaveChangesAsync();
                return RedirectToAction("FormationPanel", "FormationPages");
            }
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", formation.ModeId);
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name", formation.TypeId);
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "Name", formation.VilleId);
            ViewData["ThematicId"] = new SelectList(_context.Thematics, "Id", "Title", formation.ThematicId);
            return View(formation);
        }

        [Authorize(Roles = "Admin")]
        // GET: Formations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations.FindAsync(id);
            if (formation == null)
            {
                return NotFound();
            }
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", formation.ModeId);
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name", formation.TypeId);
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "Name", formation.VilleId);
            ViewData["ThematicId"] = new SelectList(_context.Thematics, "Id", "Title", formation.ThematicId);
            return View(formation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Description,Duration,StartDay,EndDay,StartTime,EndTime,Certificate,Presentation,Skills,Status,ModeId,VilleId,TypeId,ThematicId,OffreFColor,OffreFSize,OffreFBgColor,OffreFBgColorButton")] Formation formation)
        {
            if (id != formation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Formations.Update(formation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FormationPanel", "FormationPages");
            }
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", formation.ModeId);
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name", formation.TypeId);
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "Name", formation.VilleId);
            ViewData["ThematicId"] = new SelectList(_context.Thematics, "Id", "Title", formation.ThematicId);
            return View(formation);
        }

        [Authorize(Roles = "Admin")]

        // GET: Formations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations
                .Include(f => f.Mode)
                .Include(f => f.Type)
                .Include(f => f.Ville)
                .Include(f => f.Thematic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // POST: Formations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Formations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Formations'  is null.");
            }
            var formation = await _context.Formations.Include(f => f.Registration).FirstOrDefaultAsync(m => m.Id == id);
            if (formation != null)
            {
                _context.Formations.Remove(formation);
            }

            foreach (var f in formation.Registration)
            {
                _context.Remove(f);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("FormationPanel", "FormationPages");
        }

        private bool FormationExists(string id)
        {
            return _context.Formations.Any(e => e.Id == id);
        }


        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Nom,Prenom,Email,Phone,JobRole,CompanyName,Region,Ville,FormationName,FormationId")] Registration registration)
        {
            var formInfo = registration;
            if (ModelState.IsValid)
            {
                //var formation = registration.Formation.Title;
                //var email = registration.Email;
                //var prenom = registration.Prenom;

                await _context.AddAsync(formInfo);
                await _context.SaveChangesAsync();
            }
            await _emailSender.SendEmailAsync(formInfo.Email, "Confirmation d'inscription à la formation :  " + formInfo.FormationName + " " ,
                    $"<h3>Cher/Chère "+formInfo.Prenom+" , </h3>" +
                    "<p>Nous sommes ravis de vous informer que votre inscription à la formation "+ formInfo.FormationName + "  a été confirmée avec succès.</p>" +
                    "<p>L'équipe de Simplon Academy vous contactera dans les 72 heures qui suivent</p>" +
                    "<div>Si vous souhaitez prendre un rendez-vous adapté à votre disponibilité, veuillez sélectionner la date et l’horaire qui vous conviennent sur notre " +
                    "<a href='https://calendar.app.google/nqtvugcFKjyzRpFC9'>agenda.</a></div>" +
                    "<h5>À très vite!</h5>L'équipe Simplon Academy.");

            return RedirectToAction("Details", "Formations", new { id = registration.FormationId });
            //return View(registration);
        }

        public IActionResult ExcelInscrits(string? Id)
        {
            var inscrits = _context.Registrations.Include(x => x.Formation).Where(x => x.FormationId == Id).ToList();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");
            var currentRow = 1;
            var id = 0;
            string? formationName = null;

            worksheet.Row(currentRow).Height = 25.0;
            worksheet.Row(currentRow).Style.Font.Bold = true;
            worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Cell(currentRow, 1).Value = "Id";

            worksheet.Cell(currentRow, 2).Value = "Nom";
            worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 3).Value = "Prenom";
            worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 4).Value = "Phone";
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 5).Value = "JobRole";
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 6).Value = "CompanyName";
            worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 7).Value = "Pays";
            worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 8).Value = "Ville";
            worksheet.Cell(currentRow, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 9).Value = "Formation";
            worksheet.Cell(currentRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


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

                worksheet.Cell(currentRow, 4).Value = inscr.Phone;
                worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 5).Value = inscr.JobRole;
                worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 6).Value = inscr.CompanyName;
                worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 7).Value = inscr.Region;
                worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 8).Value = inscr.Ville;
                worksheet.Cell(currentRow, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 9).Value = inscr.Formation.Title;
                worksheet.Cell(currentRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                worksheet.Columns().AdjustToContents();
                formationName = inscr.Formation.Title;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Formation["+ formationName+"]SimplonAcademy.xlsx");
        }

        [Authorize(Roles = "Admin")]
        // POST: Formations/DeleteRegister
        [HttpPost]
        public async Task<IActionResult> DeleteRegister(string id)
        {
            var register = await _context.Registrations.FirstOrDefaultAsync(m => m.Id == id);

            if (register != null)
            {
                _context.Remove(register);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("FormationPanel", "FormationPages");

        }

        [HttpPost]
        public async Task<IActionResult> StatusAction(bool? stu, string? id)
        {
            var formation = await _context.Formations.FindAsync(id);
            formation.Status = stu;
            await _context.SaveChangesAsync();
            return RedirectToAction("FormationPanel", "FormationPages");
        }
    }
}
