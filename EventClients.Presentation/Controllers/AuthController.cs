using Entities.Models;
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
        

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationDto registration)
        {
            var user = new User
            {
                Email = registration.Email,
            };             
            
            var result = await _service.AuthService.RegisterUser(registration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            
            // Send confirmation email
            var emailSent = await _service.EmailService.SendConfirmationEmailAsync(user);
            if (!emailSent)
            {
                #pragma warning disable CS8604 // Possible null reference argument.
                await _service.AuthService.DeleteUnregisteredUser(user.Email);
                return BadRequest("Email Not Sent, Check Your Network and Try Again Please.");
            }
            return Ok(new { 
                    message = "Registration successful. Please check your email to confirm your account." 
                });
            
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegistrationDto registration)
        {
            var user = new User
            {
                Email = registration.Email,
            }; 
            var result = await _service.AuthService.RegisterAdmin(registration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            // Send confirmation email
            var emailSent = await _service.EmailService.SendConfirmationEmailAsync(user);
            if (!emailSent)
            {
                await _service.AuthService.DeleteUnregisteredUser(user.Email);
                return BadRequest("Email Not Sent, Check Your Network and Try Again Please.");
            }
            return Ok(new { 
                    message = "Admin Registration successful. Please check your email to confirm your account." 
                });
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

        [HttpPost("user-login")]

        public async Task<IActionResult> ValidateUser([FromBody] AuthDto authDto)
        {
            var user = await _service.AuthService.ValidateUser(authDto);

            if (false)

                throw new Exception("");
            
            return Ok(user.UserData);
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

        [HttpPost("send-confirmation-email")]
        public async Task<IActionResult> SendToken([FromBody] AuthDto authDto)
        {
            var user = new User
            {
                Email = authDto.Email,
            }; 

            await _service.AuthService.ValidateUser(authDto);
            if (false)
                throw new Exception("incorrect credentials!!");

            await _service.EmailService.SendConfirmationEmailAsync(user);
            return Ok(new { 
                    message = "Please check your email to confirm your account." 
                });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _service.AuthService.ConfirmEmail(email, token);
            
            if (result.Succeeded)
                return Ok("Email Confirmed Successfully");
                
            return BadRequest(result.Errors);
        }

        


    }

    
}
