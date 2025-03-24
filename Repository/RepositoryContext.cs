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
                
                modelBuilder.ApplyConfiguration(new EventConfiguration());
<<<<<<< HEAD
<<<<<<< HEAD
                base.OnModelCreating(modelBuilder);
            // Configure entity relationships and constraints here, if needed.
=======
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
>>>>>>> origin/tickets-api
=======
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
>>>>>>> 7854b3f4ba583ebb3023079cf3fc024da625d332
            }
            public DbSet<Event>? Events { get; set; }

            public DbSet<Ticket> Tickets { get; set; }  // DbSet for tickets
        public DbSet<Payment> Payments { get; set; }  // DbSet for payments


    }
}
