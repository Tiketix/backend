
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class AuthService : IAuthService

    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;


        private User? _user;

        public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<IdentityResult> RegisterUser(RegistrationDto registration)
        {
            var user = _mapper.Map<User>(registration);

                #pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userManager.CreateAsync(user, registration.Password);

            return result;
        }


        public async Task<(bool, LoginDto UserData)> ValidateUser(AuthDto authDto)
        {
            _user = await _userManager.FindByEmailAsync(authDto.Email);

            var result = _user != null && await _userManager.CheckPasswordAsync(_user, authDto.Password);
            
            if (!result)
            {
                throw new Exception("incorrect credentials!!");
            }
            LoginDto userData;
            userData = _mapper.Map<LoginDto>(_user);

            return (result, userData);
        }


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> UpdateUserEmail(string email, string password, string newEmail)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var pass = await _userManager.CheckPasswordAsync(user, password);
            
            if (!pass)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            
           
                
            user.Email = newEmail;
            user.NormalizedEmail = _userManager.NormalizeEmail(newEmail);

            
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateUserPassword(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            
            // Verify the current password is correct
            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (!checkPasswordResult)
                return IdentityResult.Failed(new IdentityError { Description = "Current password is incorrect." });
            
            // Use the built-in ChangePasswordAsync method
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }



   







        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt_Key"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Email, _user.Email)
            };
            var emails = await _userManager.GetEmailAsync(_user);
            foreach (var e in emails)
            {
            claims.Add(new Claim(ClaimTypes.Email, emails));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, 
        List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

    }
}




// public async Task<IdentityResult> UpdateUserEmail(string email, string newEmail)
// {
//     var user = await _userManager.FindByEmailAsync(email);
    
//     if (user == null)
//         return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        
//     user.Email = newEmail;
//     user.NormalizedEmail = _userManager.NormalizeEmail(newEmail);
//     user.EmailConfirmed = false; // Optional: set to false if you want the user to confirm the new email
    
//     return await _userManager.UpdateAsync(user);
// }

// public async Task<IdentityResult> UpdateUserPassword(string email, string currentPassword, string newPassword)
// {
//     var user = await _userManager.FindByEmailAsync(email);
    
//     if (user == null)
//         return IdentityResult.Failed(new IdentityError { Description = "User not found." });
    
//     // Verify the current password is correct
//     var checkPasswordResult = await _userManager.CheckPasswordAsync(user, currentPassword);
//     if (!checkPasswordResult)
//         return IdentityResult.Failed(new IdentityError { Description = "Current password is incorrect." });
    
//     // Use the built-in ChangePasswordAsync method
//     return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
// }
// public async Task<IEnumerable<User>> GetAllUsers()
// {
//     return await _userManager.Users.ToListAsync();
// }






// [Route("api/[controller]")]
// [ApiController]
// public class AuthController : ControllerBase
// {
//     private readonly IAuthService _authService;

//     public AuthController(IAuthService authService)
//     {
//         _authService = authService;
//     }

//     [HttpPost("register")]
//     public async Task<IActionResult> RegisterUser([FromBody] RegistrationDto registrationDto)
//     {
//         var result = await _authService.RegisterUser(registrationDto);
        
//         if (result.Succeeded)
//             return StatusCode(201);
            
//         return BadRequest(result.Errors);
//     }

//     [HttpGet("users")]
//     [Authorize(Roles = "Admin")] // Optional: restrict access to admins only
//     public async Task<IActionResult> GetAllUsers()
//     {
//         var users = await _authService.GetAllUsers();
//         return Ok(users);
//     }

//     [HttpPut("email")]
//     [Authorize]
//     public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailDto updateEmailDto)
//     {
//         var result = await _authService.UpdateUserEmail(
//             updateEmailDto.CurrentEmail,
//             updateEmailDto.NewEmail);
            
//         if (result.Succeeded)
//             return NoContent();
            
//         return BadRequest(result.Errors);
//     }

//     [HttpPut("password")]
//     [Authorize]
//     public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
//     {
//         var result = await _authService.UpdateUserPassword(
//             updatePasswordDto.Email,
//             updatePasswordDto.CurrentPassword,
//             updatePasswordDto.NewPassword);
            
//         if (result.Succeeded)
//             return NoContent();
            
//         return BadRequest(result.Errors);
//     }

//     [HttpPost("login")]
//     public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
//     {
//         var (signInResult, userData) = await _authService.LoginUser(loginDto);
        
//         if (signInResult.Succeeded)
//             return Ok(userData);
//         else if (signInResult.IsLockedOut)
//             return StatusCode(423, "Account is locked out");
//         else if (signInResult.IsNotAllowed)
//             return Forbidden("Login not allowed");
//         else
//             return Unauthorized();
//     }
// }


// public class UpdateEmailDto
// {
//     [Required]
//     [EmailAddress]
//     public string CurrentEmail { get; set; }
    
//     [Required]
//     [EmailAddress]
//     public string NewEmail { get; set; }
// }

// public class UpdatePasswordDto
// {
//     [Required]
//     [EmailAddress]
//     public string Email { get; set; }
    
//     [Required]
//     public string CurrentPassword { get; set; }
    
//     [Required]
//     [MinLength(8)]
//     public string NewPassword { get; set; }
// }