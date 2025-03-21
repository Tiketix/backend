using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace EventClients.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthController(IServiceManager service) => _service = service;
        

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationDto registration)
        {
        var result = await _service.AuthService.RegisterUser(registration);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
            ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
        return Ok("User Created successfully");
        }
        
        
        [HttpPost("admin-login")]

        public async Task<IActionResult> ValidateAdmin([FromBody] AuthDto authDto)
        {
            var user = await _service.AuthService.ValidateUser(authDto);
            // Console.WriteLine($"after validation {isUserValid}");
            if (false)

                throw new Exception("incorrect credentials!!");
            
            return Ok(new 
            { Token = await _service.AuthService.CreateToken(),
              user.UserData });
        }

        [HttpPost("client-login")]

        public async Task<IActionResult> ValidateUser([FromBody] AuthDto authDto)
        {
            var user = await _service.AuthService.ValidateUser(authDto);

            if (false)

                throw new Exception("");
            
            return Ok(user.UserData);
        }


        [HttpGet("get-all-users")]
        [Authorize(Roles ="Manager, Administrator")] // restrict access to admins only
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.AuthService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut("change-email")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateEmailDto updateEmailDto)
        {
            var result = await _service.AuthService.UpdateUserEmail(
                updateEmailDto.CurrentEmail,
                updateEmailDto.Password,
                updateEmailDto.NewEmail);
                
            if (result.Succeeded)
                return Ok("Email Updated Successfully");
                
            return BadRequest(result.Errors);
        }

        [HttpPut("change-phone-number")]
        public async Task<IActionResult> UpdateUserPhone([FromBody] UpdatePhoneDto updatePhoneDto)
        {
            var result = await _service.AuthService.UpdateUserPhone(
                updatePhoneDto.CurrentEmail,
                updatePhoneDto.Password,
                updatePhoneDto.PhoneNumber);
                
            if (result.Succeeded)
                return Ok("Phone Number Updated Successfully");
                
            return BadRequest(result.Errors);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordDto updateuserPasswordDto)
        {
            var result = await _service.AuthService.UpdateUserPassword(
                updateuserPasswordDto.Email,
                updateuserPasswordDto.CurrentPassword,
                updateuserPasswordDto.NewPassword);
                
            if (result.Succeeded)
                return Ok($"Password Changed Successfully. Your new password is {updateuserPasswordDto.NewPassword}");
                
            return BadRequest(result.Errors);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string email, string password)
        {
            await _service.AuthService.DeleteUser(email, password);

            return Ok("Account Deleted successfully");
        }


        [HttpDelete("admin-delete-user")]
        [Authorize(Roles ="Manager, Administrator")]
        public async Task<IActionResult> AdminDeleteUser(string email)
        {
            await _service.AuthService.AdminDeleteUser(email);

            return Ok("Account Deleted successfully");
        }

    }

    
}
