using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = false;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.None;
        });

        services.AddAuthorizationBuilder();
        
        services.AddDbContext<SportyBuddiesIdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("IdentityDatabase")));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<SportyBuddiesIdentityDbContext>()
            .AddApiEndpoints();

        services.AddScoped<UserManager<ApplicationUser>, CustomUserManager>();
        services.AddScoped<IdentityEventsHandler>();

        return services;
    }
}