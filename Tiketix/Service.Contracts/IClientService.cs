using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IClientService
{
    Task<IEnumerable<LoginDto>> GetAllUsers();
    Task<LoginDto> GetUserByEmail(string email);
    Task<IdentityResult> AdminDeleteUser(string email);

   
}
