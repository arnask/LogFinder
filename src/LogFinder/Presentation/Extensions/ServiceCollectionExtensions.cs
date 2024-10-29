using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LogFinder.Presentation.Extensions;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures settings.
    /// </summary>
    public static IServiceCollection ConfigureSettings<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class
    {
        services.Configure<T>(configuration.GetSection(sectionName));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<T>>().Value);

        return services;
    }
}
