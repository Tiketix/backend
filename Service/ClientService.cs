using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class ClientService : IClientService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;


    
    public ClientService(UserManager<User> userManager, IMapper mapper)  
    {
        _userManager = userManager;
        _mapper = mapper;
     
    }

    public async Task<IEnumerable<LoginDto>> GetAllUsers()
    {
        try
        {
            var users = await _userManager.Users.ToListAsync();

            var usersDto = _mapper.Map<IEnumerable<LoginDto>>(users);

            return usersDto;
        }
        catch (Exception)
        {
            throw new Exception($"There is an error somewhere");
        }
    }

    public async Task<LoginDto> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new Exception("Email does not exist in Database");
        var userDto = _mapper.Map<LoginDto>(user);
                    
        return userDto;
    }

    public async Task<IdentityResult> AdminDeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });

            
            return await _userManager.DeleteAsync(user);    
        }


    

}

