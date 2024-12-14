using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Profile.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        return services;
    }
}