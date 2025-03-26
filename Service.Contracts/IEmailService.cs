using Entities.Models;


namespace Service.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationEmailAsync(User user);
    }
}


