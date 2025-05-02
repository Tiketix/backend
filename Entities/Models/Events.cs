using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Event
    {
        [Column("EventId")]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Event Title is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Title is 100 characters.")]
        public required string EventTitle { get; set; }
        
        [Required(ErrorMessage = "Event Description is a required field.")]
        [MinLength(30, ErrorMessage = "Minimum length for the Event's Description is 30 characters.")]
        public required string EventDescription { get; set; }

        [Required(ErrorMessage = "Organizer Email is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Email is 100 characters.")]
        public required string OrganizerEmail { get; set; }
        
        [Required(ErrorMessage = "Organizer Phone number is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the phone number is 20 characters.")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Where is your event taking place?")]
        public required string Location { get; set; }

        [Required(ErrorMessage = "How many tickets are available??")]
        public required int TicketsAvailable { get; set; }

        [Required(ErrorMessage = "Event Date is a required field.")]
        public required DateOnly EventDate { get; set; }

        [Required(ErrorMessage = "Event Time is a required field.")]
        public required string EventTime { get; set; }



        // Foreign key to user who created event.
        [ForeignKey("Email")]
        public string? Email { get; set; }

        //Navigation property to User
        public virtual User? EventCreator { get; set; }

        //Navigation property for tickets associated to event.
        public virtual ICollection<Ticket>? Tickets { get; set; }

    }
}