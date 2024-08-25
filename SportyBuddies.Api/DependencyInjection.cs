using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportyBuddies.Api.Common.Errors;
using SportyBuddies.Api.Common.Mapping;

namespace SportyBuddies.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, SportyBuddiesProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}