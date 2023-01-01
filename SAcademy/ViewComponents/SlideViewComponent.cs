using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SlideViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _context.Slides.Include(x => x.Images).ToListAsync();
            return View(about);
        }
    }
}
