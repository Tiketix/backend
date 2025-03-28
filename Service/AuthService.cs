
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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

        public async Task<IdentityResult> RegisterAdmin(AdminRegistrationDto registration)
        {
            var user = _mapper.Map<User>(registration);

            await _userManager.CreateAsync(user, registration.Password);

            var result = await _userManager.AddToRolesAsync(user, registration.Roles);
            
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

        public async Task<IdentityResult> UpdateUserEmail(string email, string password, string newEmail)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });

            var pass = await _userManager.CheckPasswordAsync(user, password);
            
            if (!pass)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });
            
           
                
            user.Email = newEmail;
            user.NormalizedEmail = _userManager.NormalizeEmail(newEmail);

            
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateUserPhone(string email, string password, string newPhoneNumber)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });

            var pass = await _userManager.CheckPasswordAsync(user, password);
            
            if (!pass)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });
            
           
                
            user.PhoneNumber = newPhoneNumber;
            
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateUserPassword(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });
            
            // Verify the current password is correct
            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (!checkPasswordResult)
                return IdentityResult.Failed(new IdentityError { Description = "Current password is incorrect." });
            
            // Use the built-in ChangePasswordAsync method
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityResult> DeleteUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });

            var pass = await _userManager.CheckPasswordAsync(user, password);
            
            if (!pass)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });
            
            return await _userManager.DeleteAsync(user);
           
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid email confirmation parameters." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            // Decode the token
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            // Confirm email
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Email Confirmation Failed." });
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

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
            claims.Add(new Claim(ClaimTypes.Role, role));
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


