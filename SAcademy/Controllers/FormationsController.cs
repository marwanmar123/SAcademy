using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
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

        public FormationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Formations
        public async Task<IActionResult> Index()
        {
            var formation = await _context.Formations.AsNoTracking().Include(f => f.Thematic).Include(f => f.Mode).Include(f => f.Ville).Include( f => f.Type).ToListAsync();
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
                var select = query.Where(f => villes.Contains(f.VilleId) || themes.Contains(f.ThematicId) || modes.Contains(f.ModeId) );
                
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

            return View(formation);
        }

        [Authorize]
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

        [Authorize]
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
                    _context.Update(formation);
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

        [Authorize]

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
        public async Task<IActionResult> Register([Bind("Id,Nom,Prenom,Email,Phone,JobRole,CompanyName,Region,Ville,FormationId")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Formations", new { id = registration.FormationId });
            }
            return View(registration);
        }

        [Authorize]
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
