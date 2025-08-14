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


        [HttpPost("registerClient")]
        public async Task<IActionResult> RegisterClient([FromBody] RegistrationDto registration)
        {
            var result = await _service.AuthService.RegisterClient(registration);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpPost("registerAdminUser")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegistrationDto registration)
        {
            var result = await _service.AuthService.RegisterAdmin(registration);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("registerEventOrganizer")]
        public async Task<IActionResult> RegisterEventOrganizer([FromBody] RegistrationDto registration)
        {
            var result = await _service.AuthService.RegisterEventOrganizer(registration);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }


        [HttpPost("adminLogin")]

        public async Task<IActionResult> ValidateAdmin([FromBody] AuthDto authDto)
        {
            var user = await _service.AuthService.UserLogin(authDto);
            // Console.WriteLine($"after validation {isUserValid}");
            if (!user.Success)
                return BadRequest(user);

            return Ok(new
            {
                Token = await _service.AuthService.CreateToken(),
                user
            });
        }

        [HttpPost("userLogin")]

        public async Task<IActionResult> ValidateUser([FromBody] AuthDto authDto)
        {
            var user = await _service.AuthService.UserLogin(authDto);

            if (!user.Success)
                return BadRequest(user);

            return Ok(new
            {
                Token = await _service.AuthService.CreateToken(),
                user
            });
        }

        [HttpPut("updateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] LoginDto request, Guid id)
        {
            var response = await _service.AuthService.UpdateUserDetails(request, id);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordDto dto)
        {
            var response = await _service.AuthService.UpdateUserPassword(dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("requestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetDto request)
        {
            var response = await _service.AuthService.RequestPasswordReset(request);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordReset request)
        {
            var response = await _service.AuthService.ResetPassword(request);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // [HttpDelete("deleteUser")]
        // public async Task<IActionResult> DeleteUser(string email)
        // {
        //     var response = await _service.AuthService.DeleteUser(email);
        //     if (!response.Success)
        //         return BadRequest(response);

        //     return Ok(response);
        // }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _service.AuthService.ValidateToken(email, token);

            if (result.Success)
                return Ok("Email Confirmed Successfully");

            return BadRequest(result.Errors);
        }
        
        [HttpGet("sendToken")]
        public async Task<IActionResult> SendToken(string email)
        {
            var response = await _service.AuthService.SendToken(email);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        


    }

    
}
