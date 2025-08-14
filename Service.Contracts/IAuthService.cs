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
        Task<ApiResponse<string>> RequestPasswordReset(RequestPasswordResetDto request);
        Task<ApiResponse<string>> ResetPassword(PasswordReset request);
        Task<ApiResponse<bool>> ValidateToken(string email, string token);
        Task<ApiResponse<bool>> UpdateUserPassword(UpdateUserPasswordDto dto);
        // Task<ApiResponse<LoginDto>> DeleteUser(string email, string password);

        // Task<ApiResponse<LoginDto>> DeleteUser(string email);

        Task<ApiResponse<LoginDto>> UserLogin(AuthDto authDto);
        Task<ApiResponse<bool>> SendToken(string email);

        Task<string> CreateToken();

        // Task<IdentityResult> ConfirmEmail(string userId, string token);
    }
}





