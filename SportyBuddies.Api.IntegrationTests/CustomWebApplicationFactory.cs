using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's ApplicationDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<SportyBuddiesDbContext>));

            if (descriptor != null) services.Remove(descriptor);

            // Add ApplicationDbContext using an in-memory database for testing.
            services.AddDbContext<SportyBuddiesDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Build the service provider.
            var serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (ApplicationDbContext).
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SportyBuddiesDbContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();
            }
        });
    }
}