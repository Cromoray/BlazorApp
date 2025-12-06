using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedProject.Models;

namespace Server.Configurator;

public class TaskConfigurator : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("tasks");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.ExpireDate).IsRequired();
    }
}
