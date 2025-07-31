namespace Contracts;

public interface IRepositoryManager
{
    IEventRepository Event{ get; }
    ITicketRepository Ticket { get; }
    IEmailVerificationTokenRepository EmailVerificationToken{ get; }
    Task Save();
}
