using Azure;
using Novella.Data.Services;
using Novella.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace Novella.Services
{
    public class EmailService : IEmailService

    {

        private readonly IConfiguration _configuration;



        public EmailService(IConfiguration configuration)

        {

            _configuration = configuration;

        }



        public Task<SendGrid.Response> SendSingleEmail(ComposeEmailModel payload)

        {

            var apiKey = _configuration.GetSection("SendGrid")["ApiKey"];

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("suchysilvio@gmail.com", "Silvio Suchy");

            var subject = payload.Subject;

            var to = new EmailAddress(payload.Email

                                     , $"{payload.FirstName} {payload.LastName}");

            var textContent = payload.Body;

            var htmlContent = $"<strong>{payload.Body}</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject

                                                    , textContent, htmlContent);

            var request = client.SendEmailAsync(msg);

            request.Wait();

            var result = request.Result;

            return request;

        }

    }
}
