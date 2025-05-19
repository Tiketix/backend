using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Ticket
    {
        [Column("TicketId")]
        public Guid TicketId { get; set; }
        public DateTime PurchaseTime { get; set; }

        public string? EventTitle => EventDetails?.EventTitle;
        public string? OrganizerEmail => EventDetails?.OrganizerEmail;
        public double? TicketPrice => EventDetails?.TicketPrice;
        public string? LastName => Purchaser?.LastName;
        public string? FirstName => Purchaser?.FirstName;


        //foreign key to user
        [ForeignKey("UserId")]
        public string? UserId { get; set; }

        //foreign key to event
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("EventId")]
        public Guid EventId { get; set; }

        

        //navigation properties
        
        public required virtual Event EventDetails { get; set; }
        public required virtual User Purchaser { get; set; }
    }
}