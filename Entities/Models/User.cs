using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        //Navigation Properties
        public virtual ICollection<Event>? CreatedEvents { get; set; }
        public virtual ICollection<Ticket>? PurchasedTickets { get; set; }
    }

    public class UserEventTicket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public int EventId { get; set; }
        public Event? Event { get; set; }

        public int TicketCount { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
