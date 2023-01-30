using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Models;

namespace SAcademy.ViewComponents
{
    public class ContactFooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ContactFooterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contact = await _context.Contacts.Select(c => new Contact()
            {
                Localisation = c.Localisation,
                Email = c.Email,
                Call = c.Call
            }).ToListAsync();
            return View(contact);
        }
    }
}