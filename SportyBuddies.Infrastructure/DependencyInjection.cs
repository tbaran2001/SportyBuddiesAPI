using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Clock;
using SportyBuddies.Infrastructure.Repositories;
using SportyBuddies.Infrastructure.Services;

namespace SportyBuddies.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SportyBuddiesDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddScoped<ISportsRepository, SportsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IMatchesRepository, MatchesRepository>();
        services.AddScoped<IBuddiesRepository, BuddiesRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();

        services.AddScoped<IFileStorageService, FileStorageService>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<SportyBuddiesDbContext>());
        
        AddCaching(services, configuration);
        
        //AddApiVersioning(services);

        return services;
    }
    
    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}