using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public NavbarViewComponent(ApplicationDbContext context)
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
