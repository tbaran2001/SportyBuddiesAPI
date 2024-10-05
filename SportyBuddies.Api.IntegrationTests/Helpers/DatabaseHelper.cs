using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Identity;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Helpers;

public static class DatabaseHelper
{
    public static async Task<SportyBuddiesDbContext> InitializeDatabaseAsync(
        SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    {
        var scope = appFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<SportyBuddiesDbContext>();
        var dbIdentity = scopedServices.GetRequiredService<SportyBuddiesIdentityDbContext>();

        await db.Database.MigrateAsync();
        await dbIdentity.Database.MigrateAsync();
        
        await db.Database.EnsureCreatedAsync();
        await dbIdentity.Database.EnsureCreatedAsync();
        
        return db;
    }
}