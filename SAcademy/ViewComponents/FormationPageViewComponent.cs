using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class FormationPageViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public FormationPageViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var formationPages = await _context.FormationPages.ToListAsync();
            return View(formationPages);
        }
    }
}
