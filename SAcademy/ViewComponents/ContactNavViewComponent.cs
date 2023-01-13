using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Models;

namespace SAcademy.ViewComponents
{
    public class ContactNavViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ContactNavViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contact = await _context.Contacts.Select(c => new Contact()
            {
                Call = c.Call,
                Email = c.Email,
                Localisation = c.Localisation
            }).ToListAsync();
            return View(contact);
        }
    }
}
