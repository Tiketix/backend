using Entities.Models;

namespace Contracts
{
    // Defines data access methods for ticket operations.
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int userId);
        Task<IEnumerable<Ticket>> GetTicketsByEventAsync(int eventId);
        Task<bool> CancelTicketAsync(int ticketId);
        Task AddTicketAsync(Ticket ticket);
        Task SaveChangesAsync();
    }
}
