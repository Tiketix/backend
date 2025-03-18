
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
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


        public async Task<bool> ValidateUser(AuthDto authDto)
        {
            _user = await _userManager.FindByEmailAsync(authDto.Email);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, authDto.Password);
                 
            return result;
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

//add the jwt to your normal services.

//add env
//   "firstName": "Badboy",
//   "lastName": "Tee",
//   "username": "badboytee",
//   "password": "badboytee123",
//   "email": "badboyyyyyyyyyfjyjh",
//   "phoneNumber": "0205946968768"