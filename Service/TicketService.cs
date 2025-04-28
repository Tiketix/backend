
using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class TicketService : ITicketService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;


    public TicketService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public IEnumerable<TicketDto> GetAllTickets(string email, bool trackChanges)
    {
        try
        {
            var tickets = _repository.Ticket.GetAllTickets(email, trackChanges);
            var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);
            return ticketsDto;
        }
        catch (Exception)
        {
            throw new Exception($"There is an error somewhere");
        }
    }
}