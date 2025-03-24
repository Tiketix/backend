using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Represents a payment entity.
    public class Payment
    {
        public int Id { get; set; }                // Unique identifier for the payment
        public int UserId { get; set; }            // ID of the user making the payment
        public int TicketId { get; set; }          // ID of the associated ticket
        public decimal Amount { get; set; }        // Payment amount
        public DateTime PaymentDate { get; set; }  // Date and time of the payment\n 
       public required string Status { get; set; }         // Payment status (e.g., Processed, Refunded)\n      }\n  }\n  "}
   }
}