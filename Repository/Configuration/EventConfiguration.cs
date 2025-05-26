using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasData
        (
            new Event
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                EventTitle = "IT_Solutions Seminar",
                EventDescription = "It is a meeting event for people in tech",
                OrganizerEmail = "solutions@gmail.com",
                Phone = "0106896565",
                Location = "100 Brick Dr. Gwynn Oak, MD 21207",
                TicketsAvailable = 100,
                EventDate = new DateOnly(2025, 03, 25),
                EventTime = "7pm",
                TicketPrice = 10000.00
            },
            new Event
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                EventTitle = "Career_Solutions Ltd",
                EventDescription = "Boost your career forward in 20 minutes",
                OrganizerEmail = "career@gmail.com",
                Phone = "01895684",
                Location = "583 Wall Dr. Gwynn Oak, MD 21207",
                TicketsAvailable = 100,
                EventDate = new DateOnly(2025, 03, 02),
                EventTime = "7pm",
                TicketPrice = 10000.00
            }
              
        );
    }
}
}
