using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Identity;
using SportyBuddies.Infrastructure;

namespace SportyBuddies.Api.IntegrationTests;

public class SportyBuddiesWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.AddHttpContextAccessor();
            
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

            var dbIdentityContextDescriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SportyBuddiesIdentityDbContext>));
            
            if (dbIdentityContextDescriptor != null)
                services.Remove(dbIdentityContextDescriptor);
            
            var dbIdentityConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
            
            if (dbIdentityConnectionDescriptor != null)
                services.Remove(dbIdentityConnectionDescriptor);

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<SportyBuddiesIdentityDbContext>((container, options) =>
            {
                var dbConnection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(dbConnection);
            });

        });

        builder.UseEnvironment("Development");
    }
}