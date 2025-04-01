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



        // Generate a 6-digit token
        private string Generate6DigitToken()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // public string GenerateEmailConfirmationTokenAsync(string email)
        // {
        //     // Generate 6-digit token
        //     string token = Generate6DigitToken();

        //     // Create token entity
        //     var verificationToken = new EmailVerificationToken
        //     {
        //         Email = email,
        //         Token = token,
        //         ExpiryTime = DateTime.UtcNow.AddHours(1),
        //         IsUsed = false
        //     };

        //     // Save token to database
        //     _repository.EmailVerificationToken.AddToken(verificationToken);
        //     _repository.Save();

        //     return token;
        // }

        public async Task<bool> SendConfirmationEmailAsync(User user)
        {
            try 
            {
                // // Generate email confirmation token
                // var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                

                // // Encode the token to be URL-safe
                // token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                
                var token = Generate6DigitToken();

                // Create token entity
                #pragma warning disable CS8601 // Possible null reference assignment.
                var verificationToken = new EmailVerificationToken
                {
                    Email = user.Email,
                    Token = token,
                    ExpiryTime = DateTime.UtcNow.AddHours(1),
                    IsUsed = false
                };

                // Save token to database
                _repository.EmailVerificationToken.AddToken(verificationToken);
                _repository.Save();
                

                // Create confirmation link
                var confirmationLink = BuildConfirmationLink(user.Id, token);

                // Compose email content
                    #pragma warning disable CS8604 // Possible null reference argument.
                // var emailBody = CreateConfirmationEmailBody(user.Email, confirmationLink);
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
                <p>{token}</p>
                <p>If the link doesn't work, copy and paste the following URL in your browser:</p>
                <p>{confirmationLink}</p>
                <p>This link will expire in 1 hour.</p>
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


