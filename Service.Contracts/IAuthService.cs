using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegistrationDto registration);

        Task<bool> ValidateUser(AuthDto authDto);
        Task<string> CreateToken();
    }
}

