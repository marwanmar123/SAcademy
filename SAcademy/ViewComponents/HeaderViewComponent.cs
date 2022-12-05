using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public HeaderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var header = await _context.Headers.ToListAsync();
            return View(header);
        }
    }
}
