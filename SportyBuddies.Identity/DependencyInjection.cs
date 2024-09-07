using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddAuthorization();
        
        services.AddDbContext<SportyBuddiesIdentityDbContext>(options =>
            options.UseSqlite("Data Source = SportyBuddiesIdentity.db"));

        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<SportyBuddiesIdentityDbContext>();
        
        return services;
    }
}