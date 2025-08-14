
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

        public async Task<EmailVerificationTokenDto> GetToken(string email, bool trackChanges)
        {
            var token = await _repository.EmailVerificationToken.GetToken(email, trackChanges);

            var tokenDto = _mapper.Map<EmailVerificationTokenDto>(token);
            return tokenDto;
        }



        public async Task<EmailVerificationTokenDto> AddToken(AddEmailVerificationTokenDto token)
        {
            var addToken = _mapper.Map<EmailVerificationToken>(token);

            await _repository.EmailVerificationToken.AddToken(addToken);
            await _repository.Save();

            var returnToken = _mapper.Map<EmailVerificationTokenDto>(addToken);
            return returnToken;
        }
        
        public async Task<bool> RemoveToken(string email, bool trackChanges)
        {
            var token = await _repository.EmailVerificationToken.GetToken(email, trackChanges);

            await _repository.EmailVerificationToken.RemoveToken(token);
            await _repository.Save();

            return true;
        }
    }

}