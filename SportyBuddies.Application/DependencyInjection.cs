using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Application.Common.Behaviors;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            options.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        services.AddScoped<IMatchingService, MatchingService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}