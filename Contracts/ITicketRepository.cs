
using Entities.Models;

namespace Contracts
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllUserTickets(string id, bool trackChanges);
        Task AddTicket(Ticket newTicket);
        Task AddRange(IEnumerable<Ticket> tickets);
    }
}


