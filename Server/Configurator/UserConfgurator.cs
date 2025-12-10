using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedProject.Models;

namespace Server.Configurator;

public class UserConfgurator : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Password).IsRequired();
    }
}
