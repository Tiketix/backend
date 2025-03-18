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
        return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> ValidateUser([FromBody] AuthDto authDto)
        {
            var isUserValid = await _service.AuthService.ValidateUser(authDto);
            Console.WriteLine($"after validation {isUserValid}");
            if (!isUserValid)
                return Unauthorized();
            
            return Ok(new { Token = await _service
            .AuthService.CreateToken() });
        }
        
    }

    
}
