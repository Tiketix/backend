using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IClientService> _clientService;
        private readonly Lazy<IEventService> _eventService;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _clientService = new Lazy<IClientService>(() => new 
            ClientService(repositoryManager, mapper));
            _eventService = new Lazy<IEventService>(() => new
            EventService(repositoryManager, mapper));
        }
        public IClientService ClientService => _clientService.Value;
        public IEventService EventService => _eventService.Value;
    }
}
