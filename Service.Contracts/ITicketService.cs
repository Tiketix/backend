using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ITicketService
{
    IEnumerable<TicketDto> GetAllTickets(string id, bool trackChanges);

    TicketDto AddTicket(AddTicketDto newTicket);
}