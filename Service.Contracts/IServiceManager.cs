
namespace Service.Contracts;

public interface IServiceManager
{
    IClientService ClientService { get; }
    IEventService EventService { get; }
    ITicketService TicketService { get; }
    IAuthService AuthService { get; }
    IEmailService EmailService { get; }
    IEmailVerificationTokenService EmailVerificationTokenService { get; }
}
