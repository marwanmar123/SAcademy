using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class SlideTwoViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SlideTwoViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sldtwo = await _context.SlideTwos.Include(x => x.Images).ToListAsync();
            return View(sldtwo);
        }
    }
}
