using Microsoft.Extensions.DependencyInjection;

namespace FLP.Application.Configurations;

public static class ApplicationConfigurationExtension
{
    /// <summary>
    /// Configures the application services for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Register application services here
        // Example: services.AddScoped<IMyService, MyService>();
        // Add other application-specific configurations as needed
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationConfigurationExtension).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ApplicationConfiguration>());
        

        services.AddAutoMapper(typeof(ApplicationConfiguration).Assembly);

    }
}
