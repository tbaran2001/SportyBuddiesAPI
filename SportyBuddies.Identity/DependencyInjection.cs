using Microsoft.AspNetCore.Http;
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

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = false; // Set to true if you don't need JS access
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.None; // Adjust as needed
        });

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