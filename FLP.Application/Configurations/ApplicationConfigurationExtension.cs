using FLP.Application.Handlers.Exceptions;
using MediatR.Pipeline;
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
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(GlobalRequestExceptionHandler<,,>));

        services.AddAutoMapper(typeof(ApplicationConfiguration).Assembly);

    }
}
