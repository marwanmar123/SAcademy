using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class SectionThemeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SectionThemeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sThemes = await _context.SectionTheme.ToListAsync();
            return View(sThemes);
        }
    }
}
