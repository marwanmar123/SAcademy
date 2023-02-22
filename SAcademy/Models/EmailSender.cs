using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace SAcademy.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("simplonacademy3@gmail.com", "ycqpkwvpocjrzehs"),
                EnableSsl = true,
            };
            return smtpClient.SendMailAsync("simplonacademy3@gmail.com", email, subject, htmlMessage);
        }
    }
}
