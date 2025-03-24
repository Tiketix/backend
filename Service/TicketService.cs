using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    // Implements business logic for ticket operations.
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }
        
        // Books a new ticket based on the provided DTO.
        public async Task<Ticket> BookTicketAsync(TicketDto ticketDto)
        {
            var ticket = new Ticket
            {
                UserId = ticketDto.UserId,
                EventId = ticketDto.EventId,
                Price = ticketDto.Price,
                PurchaseDate = DateTime.UtcNow
            };
            await _ticketRepository.AddTicketAsync(ticket);
            await _ticketRepository.SaveChangesAsync();
            return ticket;
        }
        
        // Retrieves all tickets.
        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }
        
        // Retrieves a ticket by its ID.
        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _ticketRepository.GetTicketByIdAsync(ticketId);
        }
        
        // Retrieves tickets by user ID.
        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int userId)
        {
            return await _ticketRepository.GetTicketsByUserAsync(userId);
        }
        
        // Retrieves tickets by event ID.
        public async Task<IEnumerable<Ticket>> GetTicketsByEventAsync(int eventId)
        {
            return await _ticketRepository.GetTicketsByEventAsync(eventId);
        }
        
        // Cancels a ticket by its ID.
        public async Task<bool> CancelTicketAsync(int ticketId)
        {
            return await _ticketRepository.CancelTicketAsync(ticketId);
        }

        Task<TicketDto> ITicketService.BookTicketAsync(TicketDto ticketDto)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TicketDto>> ITicketService.GetAllTicketsAsync()
        {
            throw new NotImplementedException();
        }

        Task<TicketDto> ITicketService.GetTicketByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TicketDto>> ITicketService.GetTicketsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TicketDto>> ITicketService.GetTicketsByEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
