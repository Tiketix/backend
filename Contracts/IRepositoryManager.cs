namespace Contracts;

public interface IRepositoryManager
{
    IClientRepository Client{ get; }
    IEventRepository Event{ get; }

    void Save();
}
