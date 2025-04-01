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
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
            }
            public DbSet<Event>? Events { get; set; }
            public DbSet<EmailVerificationToken>? EmailVerificationToken { get; set; }
            

    }
}
