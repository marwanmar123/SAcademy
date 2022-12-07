using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public FooterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footer = await _context.Footers.ToListAsync();
            return View(footer);
        }
    }
}
