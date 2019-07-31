using System;
using System.Threading.Tasks;
using MimeKit;

namespace Evoflare.API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, Action<AttachmentCollection> handleAttachments = null, Action<AttachmentCollection> handleLinkedResources = null);
    }

}