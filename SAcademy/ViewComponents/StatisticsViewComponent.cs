using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class StatisticsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public StatisticsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stat = await _context.Statistics.ToListAsync();
            return View(stat);
        }
    }
}
