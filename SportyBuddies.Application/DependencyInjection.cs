﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SportyBuddies.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });


        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}