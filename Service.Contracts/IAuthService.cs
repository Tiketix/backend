using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegistrationDto registration);

        // Task<IEnumerable<User>> GetAllUsers();
        // Task<IdentityResult> UpdateUserEmail(string email, string newEmail);
        // Task<IdentityResult> UpdateUserPassword(string email, string currentPassword, string newPassword);

        Task<(bool, LoginDto UserData)> ValidateUser(AuthDto authDto);
        Task<string> CreateToken();
    }
}





