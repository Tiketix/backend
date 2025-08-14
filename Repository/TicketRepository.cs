
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;

namespace Repository
{
    internal sealed class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        // public IEnumerable<Ticket> GetAllUserTickets(string email, bool trackChanges) =>
        //     FindAll(trackChanges)
        //     .Include(t => t.Event) // load associated event
        //     .Where(t => t.UserId == id)
        //     .ToList();

        public async Task<IEnumerable<Ticket>> GetAllUserTickets(string id, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(t => t.EventDetails)
                .Include(t => t.Purchaser)
                .Where(t => t.UserId == id)
                .ToListAsync();
        }

        // public async Task<IEnumerable<Ticket>> GetAllTickets(bool trackChanges)
        // {
        //     return await FindAll(trackChanges)
        //         .Include(t => t.EventDetails)
        //         .Include(t => t.Purchaser)
        //         .ToListAsync();
        // }

        public async Task AddTicket(Ticket newTicket) => await Create(newTicket);

        public async Task AddRangeAsync(IEnumerable<Ticket> tickets) => await AddRange(tickets);
        
    }
}

