using Contracts;
using Service.Contracts;

namespace Service;

internal sealed class ClientService : IClientService
{
    private readonly IRepositoryManager _repository;
    
    public ClientService(IRepositoryManager repository)
    {
        _repository = repository;
    }
}

