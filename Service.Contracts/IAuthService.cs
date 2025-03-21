using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegistrationDto registration);

        Task<IEnumerable<LoginDto>> GetAllUsers();

        Task<IdentityResult> UpdateUserEmail(string email, string password, string newEmail);

        Task<IdentityResult> UpdateUserPhone(string email, string password, string newPhoneNumber);

        Task<IdentityResult> UpdateUserPassword(string email, string currentPassword, string newPassword);

        Task<IdentityResult> DeleteUser(string email, string password);

        Task<IdentityResult> AdminDeleteUser(string email);

        Task<(bool, LoginDto UserData)> ValidateUser(AuthDto authDto);

        Task<string> CreateToken();
    }
}





