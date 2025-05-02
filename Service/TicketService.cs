
using AutoMapper;
using Contracts;
using Entities.Models;
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

    public TicketDto AddTicket(AddTicketDto newTicket)
    {
        var addNewTicket = _mapper.Map<Ticket>(newTicket);

        _repository.Ticket.AddTicket(addNewTicket);
        _repository.Save();

        var returnTicket = _mapper.Map<TicketDto>(addNewTicket);
        return returnTicket;
    }
}