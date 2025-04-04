
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Contracts;
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
        private readonly IRepositoryManager _repository;


        private User? _user;

        public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IRepositoryManager repository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
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

            var result = await _userManager.CreateAsync(user, registration.Password);

            if(result == null)
            {
                throw new Exception("Incorrect credentials");
            }

            await _userManager.AddToRolesAsync(user, registration.Roles);
            
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
        public async Task<IdentityResult> DeleteUnregisteredUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Wrong Username or Password." });
    
            return await _userManager.DeleteAsync(user);
           
        }

        public async Task<IdentityResult> ConfirmEmail(string email, string token)
        { 
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                throw new Exception("User not found!!");
            }

            var tokenEntry = _repository.EmailVerificationToken.GetToken(email, false);

            if(tokenEntry == null)
                throw new Exception("Token is invalid!!");
            if(tokenEntry.Token != token)
                throw new Exception("Token is incorrect!!");
            if(tokenEntry.Token == token & tokenEntry.ExpiryTime < DateTime.UtcNow)
            {
                _repository.EmailVerificationToken.RemoveToken(tokenEntry);
                
                throw new Exception("Token has expired!!");
            }
            if(tokenEntry.Token == token)
            {
                // Confirm email
                user.EmailConfirmed = true;
                tokenEntry.IsUsed = true;

                if(tokenEntry.IsUsed)
                    _repository.EmailVerificationToken.RemoveToken(tokenEntry);
                
            }
            return await _userManager.UpdateAsync(user);
            //FIX EXPIRED TOKENS!!!
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


