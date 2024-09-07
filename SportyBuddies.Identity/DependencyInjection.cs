using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        services.AddAuthorizationBuilder();
        
        services.AddDbContext<SportyBuddiesIdentityDbContext>(options =>
            options.UseSqlite("Data Source = SportyBuddiesIdentity.db"));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<SportyBuddiesIdentityDbContext>()
            .AddApiEndpoints();
        
        services.AddScoped<UserManager<ApplicationUser>, CustomUserManager>();
        services.AddScoped<IdentityEventsHandler>();
        
        return services;
    }
}