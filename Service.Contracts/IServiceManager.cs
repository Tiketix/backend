
namespace Service.Contracts;

public interface IServiceManager
{
    IClientService ClientService { get; }
    IEventService EventService { get; }
    IAuthService AuthService { get; }
    IEmailService EmailService { get; }
}
