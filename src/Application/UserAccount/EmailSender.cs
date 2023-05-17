using Application.Common.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserAccount
{
   public class EmailSender : IEmailSender
    {
        private const string TemplatePath = @"EmailTemplate/{0}.html";
        private readonly Emailsettings _emailsettings;
      
        public EmailSender(IOptions<Emailsettings> emailsettings )
        {
            _emailsettings = emailsettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, int id,Register register)
        {


            Execute(email, subject,id,register).Wait();
            return Task.FromResult(0);
        }
        public async Task Execute(string email, string subject,int id,Register register)
        {
            var verify = "https://localhost:44323/verifyEmail/"+ id; 

            try
            {
                //string ToEmail = email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailsettings.UsernameEmail, "Verification mail")

                };
                mail.To.Add(_emailsettings.ToEmail);
                mail.CC.Add(_emailsettings.CcEmail);
                mail.Subject = "Verify mail" + subject;
                mail.IsBodyHtml = true;
                mail.Body = $" This is the verificatiomn Mail of:{register.Name}<br />" + $"Click this link to Verify This Email: {verify}";
    

                mail.Priority = MailPriority.High;
                using (SmtpClient smtp = new SmtpClient(_emailsettings.PrimaryDomain, _emailsettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailsettings.UsernameEmail, _emailsettings.UsernamePassward);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception cx)
            {

                string sr = cx.Message;
            }
        }


        private string GetEmailBody(string templatename)
        {
            var body = File.ReadAllText(string.Format(TemplatePath, templatename));
            return body;
        }

    }
}
