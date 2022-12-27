using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class SectionViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SectionViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _context.Sections.ToListAsync();
            return View(about);
        }
    }
}
