using AutoMapper;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class ClientService : IClientService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;


    
    public ClientService(UserManager<User> userManager, IMapper mapper)  
    {
        _userManager = userManager;
        _mapper = mapper;
     
    }

    public async Task<ApiResponse<IEnumerable<LoginDto>>> GetAllUsers()
    {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
                return ApiResponse<IEnumerable<LoginDto>>.FailureResponse(new List<string> { "No users found in the database." });

            var usersDto = _mapper.Map<IEnumerable<LoginDto>>(users);

            return ApiResponse<IEnumerable<LoginDto>>.SuccessResponse(usersDto, "Users retrieved successfully");
    }

    public async Task<ApiResponse<LoginDto>> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return ApiResponse<LoginDto>.FailureResponse(new List<string> { "User does not exist in Database" });

        var userDto = _mapper.Map<LoginDto>(user);

        return ApiResponse<LoginDto>.SuccessResponse(userDto, "User retrieved successfully");
    }

    public async Task<ApiResponse<LoginDto>> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return ApiResponse<LoginDto>.FailureResponse(new List<string> { "User does not exist in Database" });

        var userDto = _mapper.Map<LoginDto>(user);

        return ApiResponse<LoginDto>.SuccessResponse(userDto, "User retrieved successfully");
    }

    public async Task<ApiResponse<bool>> AdminDeleteUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return ApiResponse<bool>.FailureResponse(new List<string> { "User does not exist in Database" });

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
            return ApiResponse<bool>.SuccessResponse(true, "User deleted successfully");

        return ApiResponse<bool>.FailureResponse(result.Errors.Select(e => e.Description).ToList());
    }


    

}

