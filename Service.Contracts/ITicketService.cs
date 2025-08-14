using Entities.Response;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ITicketService
{
    Task<ApiResponse<UserTicketsDto>> GetAllUserTickets(GetTicketRequest request, bool trackChanges);
    Task<ApiResponse<IEnumerable<TicketDto>>> AddTicket(AddTicketDto newTicket);
}