using Microsoft.AspNetCore.Identity.UI.Services;

namespace Novella.Models
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Do nothing
            return Task.CompletedTask;
        }
    }
}
