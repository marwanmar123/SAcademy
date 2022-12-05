using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AboutViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _context.Abouts.ToListAsync();
            return View(about);
        }
    }
}
