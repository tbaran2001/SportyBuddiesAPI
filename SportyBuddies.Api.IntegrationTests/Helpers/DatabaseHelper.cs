using Microsoft.Extensions.DependencyInjection;
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
        
        await db.Database.EnsureCreatedAsync();
        
        return db;
    }
}