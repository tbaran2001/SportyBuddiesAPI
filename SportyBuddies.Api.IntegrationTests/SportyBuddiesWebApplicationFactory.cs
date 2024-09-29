using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests;

public class SportyBuddiesWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SportyBuddiesDbContext>));

            if (dbContextDescriptor != null)
                services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

            if (dbConnectionDescriptor != null)
                services.Remove(dbConnectionDescriptor);

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<SportyBuddiesDbContext>((container, options) =>
            {
                var dbConnection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(dbConnection);
            });
        });

        builder.UseEnvironment("Development");
    }
}