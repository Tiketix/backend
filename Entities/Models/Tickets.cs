using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Represents a ticket booking entity.
    public class Ticket
    {
        public int Id { get; set; }                // Unique identifier for the ticket
        public int UserId { get; set; }            // ID of the user who booked the ticket
        public int EventId { get; set; }           // ID of the event for which the ticket is booked
        public DateTime PurchaseDate { get; set; } // Date and time when the ticket was purchased
        public decimal Price { get; set; }         // Price of the ticket
    }
}
