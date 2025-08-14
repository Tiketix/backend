using Entities.Response;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IClientService
{
    Task<ApiResponse<IEnumerable<LoginDto>>> GetAllUsers();
    Task<ApiResponse<LoginDto>> GetUserByEmail(string email);
    Task<ApiResponse<LoginDto>> GetUserById(string id);
    Task<ApiResponse<bool>> AdminDeleteUser(string email);

   
}
