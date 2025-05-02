
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        // public IEnumerable<Ticket> GetAllTickets(string email, bool trackChanges) =>
        //     FindAll(trackChanges)
        //     .Include(t => t.Event) // load associated event
        //     .Where(t => t.Email == email)
        //     .ToList();

        public IEnumerable<Ticket> GetAllTickets(string email, bool trackChanges) =>
            [.. FindAll(trackChanges)
            .Include(t => t.Event) // load associated event
            .Where(t => t.Email == email)];

        public void AddTicket(Ticket newTicket) => Create(newTicket);
    }
}

