using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Application.Authentication.Commands.Register;
using SportyBuddies.Application.Authentication.Common;
using SportyBuddies.Application.Common.Behaviors;
using ErrorOr;
using FluentValidation;

namespace SportyBuddies.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}