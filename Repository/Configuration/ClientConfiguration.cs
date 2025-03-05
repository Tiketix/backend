using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasData
        (
            new Client
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                FirstName = "Tobi",
                LastName = "Temi",
                Email = "tobi@gmail.com",
                Phone = "090565695",
                Password = "jdjkjf55"
            },
            new Client
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                FirstName = "Ore",
                LastName = "Debby",
                Email = "oredebby@gmail.com",
                Phone = "0209595986",
                Password = "diuwdj293"
            }
        );
    }
}
}
