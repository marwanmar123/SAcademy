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
            var applicationDbContext = _context.Formations.AsNoTracking().Include(f => f.Mode).Include(f => f.Type).Include(f => f.Ville);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FormationsAPI
        public async Task<IActionResult> GetFormationAPI()
        {
            var applicationDbContext = _context.Formations.AsNoTracking();
            return Ok(await applicationDbContext.ToListAsync());
        }

        // GET: FormationsByFilter
        public async Task<IActionResult> GetFormationsByFilter(string? formationTypeId)
        {
            var formations = await _context.Formations.AsNoTracking().Include(f => f.Type).Where(f => f.TypeId == formationTypeId).Select(s => new Formation
            {
               Id = s.Id,
               Title = s.Title,
               Duration = s.Duration,
               TypeId = s.TypeId,
               OffreFColor= s.OffreFColor,
               OffreFBgColor= s.OffreFBgColor,
               OffreFSize= s.OffreFSize,
               OffreFBgColorButton= s.OffreFBgColorButton
               
            }).ToListAsync();
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
                .Include(f => f.Registration)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // GET: Formations/Create
        public IActionResult Create()
        {
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.FTypes, "Id", "Name");
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Duration,StartDay,EndDay,StartTime,EndTime,Certificate,Presentation,Skills,ModeId,VilleId,TypeId,OffreFColor,OffreFSize,OffreFBgColor,OffreFBgColorButton")] Formation formation)
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
            return View(formation);
        }

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
            return View(formation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Description,Duration,StartDay,EndDay,StartTime,EndTime,Certificate,Presentation,Skills,ModeId,VilleId,TypeId,OffreFColor,OffreFSize,OffreFBgColor,OffreFBgColorButton")] Formation formation)
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
            return View(formation);
        }

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

        // POST: Formations/DeleteRegister
        [HttpPost]
        public async Task<IActionResult> DeleteRegister(string id)
        {
            var register = await _context.Registrations.FirstOrDefaultAsync(m => m.Id == id);

            if(register != null)
            {
                _context.Remove(register);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("FormationPanel", "FormationPages");

        }
    }
}
