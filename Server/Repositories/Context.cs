using Microsoft.EntityFrameworkCore;
using Server.Configurator;
using SharedProject.Models;

namespace Server.Repositories;

public class Context : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TaskConfigurator());
    }
}
