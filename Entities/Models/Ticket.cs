using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Ticket
    {
        [Column("TicketId")]
        public Guid TicketId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseTime { get; set; }

        public string? EventTitle { get; set; }



        //foreign key to event

        [ForeignKey("EventId")]
        public Guid EventId { get; set; }
        


        //foreign key to user
        [ForeignKey("UserId")]
        public string? UserId { get; set;}

        //navigation properties
        
        public required virtual Event Event { get; set; }

        
        public required virtual User Purchaser { get; set; }
    }
}