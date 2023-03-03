using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SAcademy.Data;
using SAcademy.Models;
using SAcademy.ViewModel;

namespace SAcademy.Controllers
{

    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var contactData = new ContactMailViewModel()
            {
                Contact = await _context.Contacts.ToListAsync(),
                Email = await _context.Emails.ToListAsync(),
            };
            return View(contactData);
        }

        [Authorize(Roles = "Admin")]
        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,TitleColor,TitleSize,FontFamily,Content,Localisation,LocalColor,Email,EmailColor,Call,CallColor,Maps,MapWidth,MapHeight,ButtonBgColor")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
        [Authorize(Roles = "admin")]
        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return PartialView("_EditContactPartialView", contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,TitleColor,TitleSize,FontFamily,Content,Localisation,LocalColor,Email,EmailColor,Call,CallColor,Maps,MapWidth,MapHeight,ButtonBgColor")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
        [Authorize(Roles = "Admin")]
        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contacts'  is null.");
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(string id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> isVisible(bool? visible, string? id)
        {

            var contact = _context.Contacts.FirstOrDefault(a => a.Id == id);
            contact.Visible = visible;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Mail(Email email)
        {
            try
            {
                var emails = new MimeMessage();
                emails.From.Add(MailboxAddress.Parse(email.From));
                emails.To.Add(MailboxAddress.Parse(email.To));
                emails.Subject = email.Subject;
                emails.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = email.Body };

                using var smtp = new SmtpClient();
                //smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(email.To, email.Password);
                smtp.Send(emails);
                smtp.Disconnect(true);

                var addMail = new Email
                {
                    Name = email.Name,
                    EmailAdress = email.Subject,
                    Body = email.Body
                };


                _context.Add(addMail);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Home");

            }


            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMail(string id)
        {
            var resId = _context.Emails.FirstOrDefault(a => a.Id == id);
            _context.Remove(resId);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Contacts");

        }
    }
}
