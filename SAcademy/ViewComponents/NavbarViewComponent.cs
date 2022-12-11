using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.ViewModel;

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
            var Nav = new NavViewModel()
            {
                Menu = await _context.Menus.ToListAsync(),
                Home = await _context.Homes.ToListAsync(),
            };
            return View(Nav);
        }
    }
}
