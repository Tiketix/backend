using Entities.Response;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthService 
    {
        Task<ApiResponse<LoginDto>> RegisterClient(RegistrationDto registration);

        Task<ApiResponse<LoginDto>> RegisterAdmin(AdminRegistrationDto registration);
        Task<ApiResponse<LoginDto>> RegisterEventOrganizer(RegistrationDto registration);
        Task<ApiResponse<LoginDto>> UpdateUserDetails(LoginDto request, Guid id);
        Task<ApiResponse<string>> RequestPasswordReset(PasswordResetDto request);
        Task<ApiResponse<string>> ResetPassword(PasswordReset request);
        Task<ApiResponse<bool>> ValidateToken(string email, string token);
        Task<IdentityResult> UpdateUserPassword(string email, string currentPassword, string newPassword);

        Task<IdentityResult> DeleteUser(string email, string password);

        Task<IdentityResult> DeleteUnregisteredUser(string email);

        Task<ApiResponse<LoginDto>> UserLogin(AuthDto authDto);

        Task<string> CreateToken();

        Task<IdentityResult> ConfirmEmail(string userId, string token);
    }
}





