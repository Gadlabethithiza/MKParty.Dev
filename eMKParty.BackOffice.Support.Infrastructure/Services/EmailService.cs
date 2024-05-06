using System;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Interfaces;
using System.Net.Mail;

namespace eMKParty.BackOffice.Support.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendAsync(EmailRequestDto request)
        {
            var emailClient = new SmtpClient("localhost");
            var message = new MailMessage
            {
                From = new MailAddress(request.From),
                Subject = request.Subject,
                Body = request.Body
            };
            message.To.Add(new MailAddress(request.To));
            await emailClient.SendMailAsync(message);
        }
    }
}

