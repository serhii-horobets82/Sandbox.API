using System;
using System.Threading.Tasks;
using Evoflare.API.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Evoflare.API.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly SmtpSettings smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message, Action<AttachmentCollection> handleAttachments = null, Action<AttachmentCollection> handleLinkedResources = null)
        {
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message
            };

            handleAttachments?.Invoke(bodyBuilder.Attachments);
            handleLinkedResources?.Invoke(bodyBuilder.LinkedResources);

            var emailMessage = new MimeMessage
            {
                Subject = subject,
                Body = bodyBuilder.ToMessageBody()
            };

            var smtpUsername = smtpSettings.SmtpUsername;
            var smtpPassword = smtpSettings.SmtpPassword;
            var smtpHostname = smtpSettings.SmtpHostname;
            var smtpPort = smtpSettings.SmtpPort;

            emailMessage.From.Add(new MailboxAddress(smtpSettings.SmtpSender, smtpSettings.SmtpUsername));
            emailMessage.To.Add(new MailboxAddress(email));

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(smtpHostname, smtpPort, SecureSocketOptions.StartTlsWhenAvailable);
                client.AuthenticationMechanisms.Remove("XOAUTH2"); // Must be removed for Gmail SMTP
                await client.AuthenticateAsync(smtpUsername, smtpPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}