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
}
