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
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IEmailService> _emailService;
        private readonly Lazy<IEmailVerificationTokenService> _emailVerificationTokenService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper,
                                UserManager<User> userManager, IConfiguration configuration)
        {
            _clientService = new Lazy<IClientService>(() => new 
            ClientService(userManager, mapper));
            _eventService = new Lazy<IEventService>(() => new
            EventService(repositoryManager, mapper));
            _authService = new Lazy<IAuthService>(() =>new 
            AuthService(mapper, userManager, configuration));
            _emailService = new Lazy<IEmailService>(() =>new 
            EmailService(userManager, repositoryManager, configuration, mapper));
            _emailVerificationTokenService = new Lazy<IEmailVerificationTokenService>(() =>new
            EmailVerificationTokenService(repositoryManager, mapper));
        }
        public IClientService ClientService => _clientService.Value;
        public IEventService EventService => _eventService.Value;
        public IAuthService AuthService => _authService.Value;
        public IEmailService EmailService => _emailService.Value;
        public IEmailVerificationTokenService EmailVerificationTokenService => _emailVerificationTokenService.Value;
    }
}
