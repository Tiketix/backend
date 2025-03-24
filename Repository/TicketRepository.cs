using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Try one of these depending on where your AppDbContext is located
using Entities; // If AppDbContext is directly in the Entities namespace
// using Data; // Alternative location
// using Persistence; // Alternative location
// using Infrastructure; // Alternative location
using Repository;

namespace Repository
{
    // Implements ticket data access using EF Core.
    public class TicketRepository : ITicketRepository
    {
        private readonly RepositoryContext _context;
        
        public TicketRepository(RepositoryContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
        
        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets.FindAsync(ticketId) ?? throw new KeyNotFoundException($"Ticket with ID {ticketId} not found.");
        }
        
        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int userId)
        {
            return await _context.Tickets.Where(t => t.UserId == userId).ToListAsync();
        }
        
        public async Task<IEnumerable<Ticket>> GetTicketsByEventAsync(int eventId)
        {
            return await _context.Tickets.Where(t => t.EventId == eventId).ToListAsync();
        }
        
        public async Task<bool> CancelTicketAsync(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}