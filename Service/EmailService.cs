using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryManager _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        

        public EmailService(UserManager<User> userManager, IRepositoryManager repository, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8601 // Possible null reference assignment.



        // Generate a 6-digit token
        private string Generate6DigitToken()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task<bool> SendConfirmationEmailAsync(User user)
        {
            try 
            {
                var token = Generate6DigitToken();

                // Create token entity
                var verificationToken = new EmailVerificationToken
                {
                    Email = user.Email,
                    Token = token,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(15),
                    IsUsed = false
                };

                // Save token to database
                await _repository.EmailVerificationToken.AddToken(verificationToken);
                await _repository.Save();
                
                // Create confirmation link
                var confirmationLink = BuildConfirmationLink(user.Id, token);

                // Compose email content
                var emailBody = CreateConfirmationEmailBody(token, confirmationLink);

                // Send email using Gmail SMTP
                await SendEmailWithGmailSmtp(
                    to: user.Email, 
                    subject: "Confirm Your Email", 
                    body: emailBody
                );

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendResetPasswordEmailAsync(User user)
        {
            try 
            {
                // Generate password reset token            
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Create token entity
                var verificationToken = new EmailVerificationToken
                {
                    Email = user.Email,
                    Token = token,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(15),
                    IsUsed = false
                };

                // Save token to database
                await _repository.EmailVerificationToken.AddToken(verificationToken);
                await _repository.Save();
                

                // Create confirmation link
                var confirmationLink = BuildConfirmationLink(user.Id, token);

                // Compose email content
                var emailBody = CreateResetPasswordEmailBody(confirmationLink);

                // Send email using Gmail SMTP
                await SendEmailWithGmailSmtp(
                    to: user.Email, 
                    subject: "Confirm Your Email", 
                    body: emailBody
                );

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }

        private string BuildConfirmationLink(string userId, string token)
        {
            // Frontend confirmation URL (adjust to match your frontend route)
            return $"http://localhost:5013/swagger/confirm-email?userId={userId}&token={token}";
        }

        private string CreateConfirmationEmailBody(string token, string confirmationLink)
        {
            return $@"
            <html>
            <body>
                <h2>Tiketix Email Confirmation</h2>
                <p>Hello,</p>
                <p>Thank you for registering. Please confirm your email by entering the code below:</p>
                <h2><p>{token}</p></h2>
                <p>If the link doesn't work, copy and paste the following URL in your browser:</p>
                <p>{confirmationLink}</p>
                <p>This token will expire in 15 minutes.</p>
                <p>Best regards,<br>Tiketix</p>
            </body>
            </html>";
        }

        private string CreateResetPasswordEmailBody(string confirmationLink)
        {
             return $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <h2>Tiketix Password Reset</h2>
                <p>Hello,</p>
                <p>You requested a password reset. Please click the button below to reset your password:</p>
                <p>
                    <a href='{confirmationLink}' target='_blank' 
                    style='display: inline-block; padding: 10px 20px; font-size: 16px; color: white; background-color: #007bff; text-decoration: none; border-radius: 5px;'>
                        Reset Password
                    </a>
                </p>
                <p>This link will expire in 15 minutes.</p>
                <p>Best regards,<br>Tiketix</p>
            </body>
            </html>";
        }


        private async Task SendEmailWithGmailSmtp(string to, string subject, string body)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(
                    _configuration["Gmail:Username"],
                    _configuration["Gmail:AppPassword"]
                );
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Gmail:Username"], "Tiketix"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}


