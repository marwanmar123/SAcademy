using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class NavFooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public NavFooterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await _context.Menus.ToListAsync();
            return View(menu);
        }
    }
}
