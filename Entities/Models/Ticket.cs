using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Ticket
    {
        [Column("TicketId")]
        public Guid Id { get; set; }

        [ForeignKey("EventId")]
        public Guid EventId { get; set; }
    }
}

