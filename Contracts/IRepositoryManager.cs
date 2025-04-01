namespace Contracts;

public interface IRepositoryManager
{
    IEventRepository Event{ get; }
    IEmailVerificationTokenRepository EmailVerificationToken{ get; }
    void Save();
}
