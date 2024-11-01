using Microsoft.EntityFrameworkCore;
using SportyBuddies.Identity;
using SportyBuddies.Infrastructure;

namespace SportyBuddies.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<SportyBuddiesDbContext>();

        dbContext.Database.Migrate();
    }
    
    public static void ApplyIdentityMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<SportyBuddiesIdentityDbContext>();

        dbContext.Database.Migrate();
    }
}