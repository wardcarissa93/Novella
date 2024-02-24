using Novella.Models;
using SendGrid;





namespace Novella.Data.Services

{

    public interface IEmailService

    {

        Task<Response> SendSingleEmail(ComposeEmailModel payload);

    }

}