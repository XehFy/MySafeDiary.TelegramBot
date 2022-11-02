using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
//using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace MySafeDiary.Domain.Abstractions
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string html);
    }
}
