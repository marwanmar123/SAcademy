using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class ThematicViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ThematicViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var thematics = await _context.Thematics.ToListAsync();
            return View(thematics);
        }
    }
}
