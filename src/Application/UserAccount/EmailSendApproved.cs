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
    public class EmailSendApproved : IEmailApproved
    {
        private readonly Emailsettings _emailSettings;
        public EmailSendApproved(IOptions<Emailsettings> emailsettings)
        {
            _emailSettings = emailsettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, Register register)
        {
            Execute(email, subject, register).Wait();
            return Task.FromResult(0);
        }

       

        public async Task Execute(string email, string subject, Register register)
        {
            var approved = "https://localhost:44323/login";

            try
            {
                string ToEmail = email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Verification mail")

                };
                mail.To.Add(ToEmail);
                mail.CC.Add(_emailSettings.CcEmail);
                mail.Subject = "Verify mail" + subject;
                mail.IsBodyHtml = true;
                mail.Body = $" Hi:{register.Name}<br />" + $"Click this link to login : {approved}";


                mail.Priority = MailPriority.High;
                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassward);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception cx)
            {

                string sr = cx.Message;
            }
        }


        //private string GetEmailBody(string templatename)
        //{
        //    var body = File.ReadAllText(string.Format(TemplatePath, templatename));
        //    return body;
        //}

    }
}

    
