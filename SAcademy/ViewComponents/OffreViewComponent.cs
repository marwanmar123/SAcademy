using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class OffreViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public OffreViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var offre = await _context.Offres.ToListAsync();
            return View(offre);
        }
    }
}
