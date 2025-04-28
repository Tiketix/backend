
using Entities.Models;

namespace Contracts
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAllTickets(string email, bool trackChanges);
    }
}


