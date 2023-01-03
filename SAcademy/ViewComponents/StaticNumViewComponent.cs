using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class StaticNumViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public StaticNumViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var statNum = await _context.StaticNums.ToListAsync();
            return View(statNum);
        }
    }
}
