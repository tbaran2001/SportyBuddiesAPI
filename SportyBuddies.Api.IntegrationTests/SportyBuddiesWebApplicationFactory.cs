using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests;

public class SportyBuddiesWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<SportyBuddiesDbContext>));

            if (descriptor != null) services.Remove(descriptor);

            // Add an in-memory database for testing
            services.AddDbContext<SportyBuddiesDbContext>(options => { options.UseSqlite("DataSource=:memory:"); });

            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (SportyBuddiesDbContext)
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<SportyBuddiesDbContext>();

            // Ensure the database is created
            db.Database.OpenConnection();
            db.Database.EnsureCreated();

            SeedDatabase(db);
        });
    }

    private void SeedDatabase(SportyBuddiesDbContext dbContext)
    {
        // Add initial data to the database
        dbContext.Sports.Add(new Sport("Football", "A sport", new List<User>()));

        dbContext.SaveChanges();
    }
}