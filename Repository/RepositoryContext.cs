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
                
                modelBuilder.ApplyConfiguration(new ClientConfiguration());
                modelBuilder.ApplyConfiguration(new EventConfiguration());
            }
            public DbSet<Client>? Clients { get; set; }
            public DbSet<Event>? Events { get; set; }

    }
}
