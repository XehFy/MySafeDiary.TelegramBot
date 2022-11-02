using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
//using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MySafeDiary.Domain.Abstractions;

namespace MySafeDiary.Domain.Services
{
    public class EmailService : IEmailService
    {
        //private readonly AppSettings _appSettings;

        public EmailService()
        {
            //_appSettings = appSettings.Value;
        }

        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("MySafeDiary@hotmail.com", "ChurbanovR!");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
