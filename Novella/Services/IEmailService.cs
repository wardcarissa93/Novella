using Azure;
using Novella.Models;
using SendGrid;




namespace Novella.Data.Services

{

    public interface IEmailService

    {

        Task<SendGrid.Response> SendSingleEmail(ComposeEmailModel payload);

    }

}