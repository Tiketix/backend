using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IClientService> _clientService;
        private readonly Lazy<IEventService> _eventService;
        private readonly Lazy<ITicketService> _ticketService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IEmailService> _emailService;
        private readonly Lazy<ICloudinaryService> _cloudinaryService;
        private readonly Lazy<IEmailVerificationTokenService> _emailVerificationTokenService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper,
                                UserManager<User> userManager, IConfiguration configuration)
        {
            _clientService = new Lazy<IClientService>(() => new
            ClientService(userManager, mapper));
            _eventService = new Lazy<IEventService>(() => new
            EventService(repositoryManager, mapper));
            _ticketService = new Lazy<ITicketService>(() => new
            TicketService(repositoryManager, mapper));
            _authService = new Lazy<IAuthService>(() => new
            AuthService(mapper, userManager, configuration, repositoryManager));
            _emailService = new Lazy<IEmailService>(() => new
            EmailService(userManager, repositoryManager, configuration, mapper));
            _emailVerificationTokenService = new Lazy<IEmailVerificationTokenService>(() => new
            EmailVerificationTokenService(repositoryManager, mapper));
            _cloudinaryService = new Lazy<ICloudinaryService>(() => new
           CloudinaryService(configuration));
        }
        public IClientService ClientService => _clientService.Value;
        public IEventService EventService => _eventService.Value;
        public ITicketService TicketService => _ticketService.Value;
        public IAuthService AuthService => _authService.Value;
        public IEmailService EmailService => _emailService.Value;
        public IEmailVerificationTokenService EmailVerificationTokenService => _emailVerificationTokenService.Value;
        public ICloudinaryService CloudinaryService => _cloudinaryService.Value;

    }
}
