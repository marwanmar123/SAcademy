using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;

namespace SAcademy.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ContactViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contact = await _context.Contacts.ToListAsync();
            return View(contact);
        }
    }
}
