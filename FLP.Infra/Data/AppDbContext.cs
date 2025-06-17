using FLP.Core.Context.Main;
using Microsoft.EntityFrameworkCore;

namespace FLP.Infra.Data;

internal class AppDbContext : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entity mappings here
        base.OnModelCreating(modelBuilder);

        // Apply configurations from the assembly where the context is defined
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // If you want to configure the context without using dependency injection,
        // you can set the connection string here.
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FunctionDemo;Trusted_Connection=True;MultipleActiveResultSets=true");
        //.UseLazyLoadingProxies(); // Enable lazy loading if needed
    }
    // Define DbSets for your entities
    public virtual DbSet<Bug> Bugs { get; set; }

    public AppDbContext() : base()
    {

    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    // public DbSet<User> Users { get; set; }
    // Add other DbSets as needed
}
