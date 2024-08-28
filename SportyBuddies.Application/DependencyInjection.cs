using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Application.Services;

namespace SportyBuddies.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISportsService, SportsService>();

        return services;
    }
}