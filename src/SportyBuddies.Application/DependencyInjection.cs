using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.Behaviors;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Services;

namespace SportyBuddies.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            options.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        services.AddScoped<IBuddyService, BuddyService>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<IConversationService, ConversationService>();
        services.AddScoped<IUserPhotoService, UserPhotoService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();

        return services;
    }
}