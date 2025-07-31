
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class AuthService : IAuthService

    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;
        private readonly IEmailService _emailService;


        private User? _user;

        public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration,
                                    IRepositoryManager repository, IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
            _emailService = emailService;
        }


#pragma warning disable CS8604 // Possible null reference argument.


        public async Task<ApiResponse<LoginDto>> RegisterClient(RegistrationDto registration)
        {
            var user = _mapper.Map<User>(registration);

            var result = await _userManager.CreateAsync(user, registration.Password);

            var response = _mapper.Map<LoginDto>(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<LoginDto>.FailureResponse(errors, "Registration failed");
            }
            registration.Roles = new List<string> { "Client" };
            await _userManager.AddToRolesAsync(user, registration.Roles);
            // Send confirmation email
            var emailSent = await _emailService.SendConfirmationEmailAsync(user);
            if (!emailSent)
            {
                // await DeleteUnregisteredUser(user.Email);
                return ApiResponse<LoginDto>.FailureResponse(new List<string> { "Email Not Sent, Check Your Network and Try Again Please. User Deleted" }, "Registration failed");
            }

            return ApiResponse<LoginDto>.SuccessResponse(response, "User registered successfully, Check email to confirm your account");
        }

        public async Task<ApiResponse<LoginDto>> RegisterEventOrganizer(RegistrationDto registration)
        {
            var user = _mapper.Map<User>(registration);

            var result = await _userManager.CreateAsync(user, registration.Password);

            var response = _mapper.Map<LoginDto>(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<LoginDto>.FailureResponse(errors, "Registration failed");
            }
            registration.Roles = new List<string> { "EventOrganizer" };
            await _userManager.AddToRolesAsync(user, registration.Roles);
            // Send confirmation email
            var emailSent = await _emailService.SendConfirmationEmailAsync(user);
            if (!emailSent)
            {
                // await DeleteUnregisteredUser(user.Email);
                return ApiResponse<LoginDto>.FailureResponse(new List<string> { "Email Not Sent, Check Your Network and Try Again Please. User Deleted" }, "Registration failed");
            }

            return ApiResponse<LoginDto>.SuccessResponse(response, "EventOrganizer registered successfully, Check email to confirm your account");
        }

        public async Task<ApiResponse<LoginDto>> RegisterAdmin(AdminRegistrationDto registration)
        {
            var user = _mapper.Map<User>(registration);

            var result = await _userManager.CreateAsync(user, registration.Password);

            var response = _mapper.Map<LoginDto>(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<LoginDto>.FailureResponse(errors, "Registration failed");
            }

            await _userManager.AddToRolesAsync(user, registration.Roles);

            // Send confirmation email
            var emailSent = await _emailService.SendConfirmationEmailAsync(user);
            if (!emailSent)
            {
                await DeleteUnregisteredUser(user.Email);
                return ApiResponse<LoginDto>.FailureResponse(new List<string> { "Email Not Sent, Check Your Network and Try Again Please. AdminUser Deleted" }, "Registration failed");
            }
            return ApiResponse<LoginDto>.SuccessResponse(response, "AdminUser registered successfully, Check email to confirm your account");
        }


        public async Task<ApiResponse<LoginDto>> UserLogin(AuthDto authDto)
        {
            _user = await _userManager.FindByEmailAsync(authDto.Email);

            var result = _user != null && await _userManager.CheckPasswordAsync(_user, authDto.Password);
            if (!result)
            {
                return ApiResponse<LoginDto>.FailureResponse(new List<string> { "Incorrect credentials!!" });
            }
            // LoginDto userData;
            var userData = _mapper.Map<LoginDto>(_user);

            return ApiResponse<LoginDto>.SuccessResponse(userData, "User Login Successful");
        }

        public async Task<ApiResponse<LoginDto>> UpdateUserDetails(LoginDto request, Guid id)
        {
            _user = await _userManager.FindByIdAsync(id.ToString());
            if (_user == null)
            {
                return ApiResponse<LoginDto>.FailureResponse(new List<string> { "User not found!" });
            }

            if (string.IsNullOrWhiteSpace(request.FirstName))
                request.FirstName = _user.FirstName;
            if (string.IsNullOrWhiteSpace(request.LastName))
                request.LastName = _user.LastName;
            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                request.PhoneNumber = _user.PhoneNumber;

            if (string.IsNullOrWhiteSpace(request.Email))
                request.Email = _user.Email;
            await _userManager.UpdateNormalizedEmailAsync(_user);

            if (string.IsNullOrWhiteSpace(request.Username))
                request.Username = _user.UserName;
            await _userManager.UpdateNormalizedUserNameAsync(_user);

            _mapper.Map(request, _user);

            var result = await _userManager.UpdateAsync(_user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<LoginDto>.FailureResponse(errors, "Update failed");
            }

            var response = _mapper.Map<LoginDto>(_user);
            return ApiResponse<LoginDto>.SuccessResponse(response, "User details updated successfully");
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

        public async Task<ApiResponse<string>> RequestPasswordReset(PasswordResetDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return ApiResponse<string>.FailureResponse(new List<string> { "User not found." });

            var emailSent = await _emailService.SendConfirmationEmailAsync(user);
            if (!emailSent)
            {
                return ApiResponse<string>.FailureResponse(new List<string> { $"Email Not Sent, Check Your Network and Try Again Please." }, "Request failed");
            }
            return ApiResponse<string>.SuccessResponse("Successful", "Password reset email sent.");

        }

        public async Task<ApiResponse<bool>> ValidateToken(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return ApiResponse<bool>.FailureResponse(new List<string> { "User not found." });

            var tokenEntry = _repository.EmailVerificationToken.GetToken(email, false);
            if (tokenEntry == null || tokenEntry.Token != token)
                return ApiResponse<bool>.FailureResponse(new List<string> { "Invalid Token." });

            if (tokenEntry.ExpiryTime < DateTime.UtcNow)
            {
                await _repository.EmailVerificationToken.RemoveToken(tokenEntry);
                return ApiResponse<bool>.FailureResponse(new List<string> { "Token has expired." });
            }

            //delete token
        
            return ApiResponse<bool>.SuccessResponse(true);
        }

        public async Task<ApiResponse<string>> ResetPassword(PasswordReset request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return ApiResponse<string>.FailureResponse(new List<string> { "User not found." });

            var token = await ValidateToken(request.Email, request.Token);
            if (!token.Success)
                return ApiResponse<string>.FailureResponse(token.Errors, "Token validation failed");

            var resetResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!resetResult.Succeeded)
            {
                var errors = resetResult.Errors.Select(e => e.Description).ToList();
                return ApiResponse<string>.FailureResponse(errors, "Password reset failed");
            }

            return ApiResponse<string>.SuccessResponse("Successful", "Password reset successfully");
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
            if (user == null)
            {
                throw new Exception("User not found!!");
            }

            var tokenEntry = _repository.EmailVerificationToken.GetToken(email, false);

            if (tokenEntry == null)
                throw new Exception("Token is invalid!!");
            if (tokenEntry.Token != token)
                throw new Exception("Token is incorrect!!");
            if (tokenEntry.Token == token & tokenEntry.ExpiryTime < DateTime.UtcNow)
            {
                await _repository.EmailVerificationToken.RemoveToken(tokenEntry);

                throw new Exception("Token has expired!!");
            }
            if (tokenEntry.Token == token)
            {
                // Confirm email
                user.EmailConfirmed = true;
                tokenEntry.IsUsed = true;

                if (tokenEntry.IsUsed)
                    await _repository.EmailVerificationToken.RemoveToken(tokenEntry);

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
        
        private string Generate6DigitToken()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

    }
}


