using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class InscriptionPageViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public InscriptionPageViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var inscriPage = await _context.InscriptionPages.ToListAsync();
            return View(inscriPage);
        }
    }
}
