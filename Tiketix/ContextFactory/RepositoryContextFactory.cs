using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace Tiketix.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //SQL SERVER
            // var builder = new DbContextOptionsBuilder<RepositoryContext>()
            // .UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"), 
            // b => b.MigrationsAssembly("Tiketix"));
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlServer(connectionString, 
            b => b.MigrationsAssembly("Tiketix"));

            //MYSQL
            // var builder = new DbContextOptionsBuilder<RepositoryContext>()
            // .UseMySql(Environment.GetEnvironmentVariable("DefaultConnection2"),
            // ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DefaultConnection2")),
            // b => b.MigrationsAssembly("Tiketix"));

            return new RepositoryContext(builder.Options);
        } 
    }
}
