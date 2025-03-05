using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{

    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) 
            { 
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new ClientConfiguration());
                modelBuilder.ApplyConfiguration(new EventConfiguration());
            }
            public DbSet<Client>? Clients { get; set; }
            public DbSet<Event>? Events { get; set; }

    }
}
