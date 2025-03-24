using Shared.DataTransferObjects;

namespace Service.Contracts
{
    // Defines business logic methods for ticket operations.
    public interface ITicketService
    {
        // Books a new ticket using the provided DTO.
        Task<TicketDto> BookTicketAsync(TicketDto ticketDto);
        
        // Retrieves all tickets.
        Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
        
        // Retrieves a specific ticket by its ID.
        Task<TicketDto> GetTicketByIdAsync(int ticketId);
        
        // Retrieves tickets for a specific user.
        Task<IEnumerable<TicketDto>> GetTicketsByUserAsync(int userId);
        
        // Retrieves tickets for a specific event.
        Task<IEnumerable<TicketDto>> GetTicketsByEventAsync(int eventId);
        
        // Cancels a ticket by its ID.
        Task<bool> CancelTicketAsync(int ticketId);
    }
}
