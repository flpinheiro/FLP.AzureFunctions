using FLP.Core.Interfaces.Repository;
using FLP.Infra.Data;
using FLP.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FLP.Infra.Configurations;

public static class InfraConfigurationExtension
{
    /// <summary>
    /// Configures the infrastructure services for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }
        // Register the DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        // Register the UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Register repositories as Lazy to avoid circular dependencies
        services.AddScopedLazy<IBugRepository, BugRepository>();
        // Register repositories
        //services.AddScoped<IBugRepository, BugRepository>();
    }

    /// <summary>
    /// Registers a service as a Lazy<T> scoped service.
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <param name="services"></param>
    private static void AddScopedLazy<TInterface, TClass>(this IServiceCollection services)
        where TInterface : class
        where TClass : class, TInterface
    {
        services.AddScoped(provider => new Lazy<TInterface>(() => provider.GetRequiredService<TInterface>()));
        services.AddScoped<TInterface, TClass>();
    }
}
