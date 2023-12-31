﻿using MimeKit;
using MailKit.Net.Smtp;
using TheJitu_Commerce_EmailService.Models.Dtos;

namespace TheJitu_Commerce_EmailService.Services
{
    public class SendMailService
    {
        public async Task SendMail(UserMessage res, string message)
        {
            MimeMessage message1 = new MimeMessage();
            message1.From.Add(new MailboxAddress("JituCommerce", "samuelwachira219@gmail.com"));

            message1.To.Add(new MailboxAddress(res.Name, res.Email));

            message1.Subject = "Welcome to Jitu Commerce";

            var body = new TextPart("html")
            {
                Text = message.ToString()
            };

            message1.Body = body;
            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("samuelwachira219@gmail.com", "ijlzzfgroyllvuqu");
            await client.SendAsync(message1);
            await client.DisconnectAsync(true);
        }
    }
}
