
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

        // public IEnumerable<Ticket> GetAllTickets(string email, bool trackChanges) =>
        //     FindAll(trackChanges)
        //     .Include(t => t.Event) // load associated event
        //     .Where(t => t.UserId == id)
        //     .ToList();

        public IEnumerable<Ticket> GetAllTickets(string id, bool trackChanges) =>
            [.. FindAll(trackChanges)
            .Include(t => t.EventDetails).Include(t => t.Purchaser)// load associated event
            .Where(t => t.UserId == id)
            ];

        public void AddTicket(Ticket newTicket) => Create(newTicket);
    }
}

