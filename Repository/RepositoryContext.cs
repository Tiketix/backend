using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{

    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) 
            { 
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                base.OnModelCreating(modelBuilder);

                //Event to user relationship
                modelBuilder.Entity<Event>()
                    .HasOne(e => e.EventCreator)
                    .WithMany(u => u.CreatedEvents)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Ticket>()
                    .HasOne(t => t.EventDetails)
                    .WithMany(e => e.Tickets)
                    .HasForeignKey(t => t.EventId);

                modelBuilder.Entity<Ticket>()
                    .HasOne(t => t.Purchaser)
                    .WithMany(u => u.PurchasedTickets)
                    .HasForeignKey(t => t.UserId);
                
                // modelBuilder.ApplyConfiguration(new EventConfiguration());
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
            }
            public DbSet<Event>? Events { get; set; }
            public DbSet<Ticket>? Ticket { get; set; }
            public DbSet<EmailVerificationToken>? EmailVerificationToken { get; set; }
            

    }
}
