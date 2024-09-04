using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Infrastructure.Common.Persistence;
using SportyBuddies.Infrastructure.Sports.Persistence;
using SportyBuddies.Infrastructure.Users.Persistence;

namespace SportyBuddies.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<SportyBuddiesDbContext>(options =>
            options.UseSqlite("Data Source = SportyBuddies.db"));
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ISportsRepository, SportsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        
        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<SportyBuddiesDbContext>());

        return services;
    }
}