using Foodico.Services.EmailAPI.Models.Dto;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using System.Net;
using Foodico.Services.EmailAPI.Models;

namespace Foodico.Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        public EmailService() { }
       
        public Task SendOrderEmailAsync(string email)
        {
            try
            {

                email = email.Trim(new char[] { '"', '/', '\\', ' ', '\t', '\n', '\r' });


                var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("herbaezgi1@gmail.com", "eorq bxme dxqi fteu")
                };
                string emailTemplatePath = "Views/OrderEmailTemplate.html";
                string emailTemplate = File.ReadAllText(emailTemplatePath);
                emailTemplate = emailTemplate.Replace("{{Email}}", email);

                return client.SendMailAsync(
                     new MailMessage(
                        from: "herbaezgi1@gmail.com",
                        to: email,
                        subject: "Order Confirmation",
                        body: emailTemplate)
                     {
                         IsBodyHtml = true
                     });
            }
            catch (Exception ex)
            {
                return null;
               
            }
        }
    }
}
