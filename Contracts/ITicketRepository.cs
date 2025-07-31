
using Entities.Models;

namespace Contracts
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAllTickets(string id, bool trackChanges);

        Task AddTicket(Ticket newTicket);
    }
}


