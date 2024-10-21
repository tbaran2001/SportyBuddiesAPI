using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Infrastructure.Buddies.Persistence;
using SportyBuddies.Infrastructure.Common.Persistence;
using SportyBuddies.Infrastructure.Matches.Persistence;
using SportyBuddies.Infrastructure.Messages.Persistence;
using SportyBuddies.Infrastructure.Services;
using SportyBuddies.Infrastructure.Sports.Persistence;
using SportyBuddies.Infrastructure.Users.Persistence;

namespace SportyBuddies.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<SportyBuddiesDbContext>(options =>
            options.UseSqlite("Data Source = SportyBuddies.db"));

        services.AddScoped<ISportsRepository, SportsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IMatchesRepository, MatchesRepository>();
        services.AddScoped<IBuddiesRepository, BuddiesRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();

        services.AddScoped<IFileStorageService, FileStorageService>();

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<SportyBuddiesDbContext>());

        return services;
    }
}