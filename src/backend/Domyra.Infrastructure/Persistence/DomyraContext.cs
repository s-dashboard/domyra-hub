namespace Domyra.Infrastructure.Persistence;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class DomyraContext : DbContext
{
    public DomyraContext(DbContextOptions<DomyraContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the assembly automatically
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DomyraContext).Assembly);
    }
    
    public DbSet<Device> Devices { get; set; }
    public DbSet<Widget> Widgets { get; set; }
}