using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace SAcademy.Models
{
    public class EmailSender : IEmailSender
    {
        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    var smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("simplonacademy@simplon.co", "oimdvwnzdfnnzaqj"),
        //        EnableSsl = true,
        //    };
        //    return smtpClient.SendMailAsync("simplonacademy@simplon.co", email, subject, htmlMessage);
        //}
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "simplonacademy@simplon.co",
                    Password = "oimdvwnzdfnnzaqj"
                };

                client.Credentials = credential;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;

                using (var message = new MailMessage())
                {
                    message.To.Add(new MailAddress(email));
                    message.From = new MailAddress("simplonacademy@simplon.co");
                    message.Subject = subject;
                    message.Body = htmlMessage;
                    message.IsBodyHtml = true;

                    await client.SendMailAsync(message);
                }
            }
        }
    }
}
