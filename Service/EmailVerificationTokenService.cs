
using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class EmailVerificationTokenService : IEmailVerificationTokenService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public EmailVerificationTokenService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public EmailVerificationTokenDto GetToken(string email, bool trackChanges)
        {
            var token = _repository.EmailVerificationToken.GetToken(email, trackChanges);

            var tokenDto = _mapper.Map<EmailVerificationTokenDto>(token);
            return tokenDto;
        }

        

        public EmailVerificationTokenDto AddToken(AddEmailVerificationTokenDto token)
        {
            var addToken = _mapper.Map<EmailVerificationToken>(token);

            _repository.EmailVerificationToken.AddToken(addToken);
            _repository.Save();

            var returnToken = _mapper.Map<EmailVerificationTokenDto>(addToken);
            return returnToken;
        }
    }

}