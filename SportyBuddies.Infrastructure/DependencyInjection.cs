using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Application.Common.Interfaces.Authentication;
using SportyBuddies.Application.Common.Interfaces.Persistence;
using SportyBuddies.Application.Common.Interfaces.Services;
using SportyBuddies.Infrastructure.Authentication;
using SportyBuddies.Infrastructure.Persistence;
using SportyBuddies.Infrastructure.Services;

namespace SportyBuddies.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}