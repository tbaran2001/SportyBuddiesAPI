﻿using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SportyBuddies.Api;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Application.SubcutaneousTests.Common;

public class MediatorFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private SqliteTestDatabase _testDatabase = null!;


    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _testDatabase.Dispose();

        return Task.CompletedTask;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _testDatabase = SqliteTestDatabase.CreateAndInitialize();

        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<SportyBuddiesDbContext>>()
                .AddDbContext<SportyBuddiesDbContext>((sp, options) => options.UseSqlite(_testDatabase.Connection));
        });
    }

    public IMediator CreateMediator()
    {
        var serviceScope = Services.CreateScope();

        _testDatabase.ResetDatabase();

        return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    }
}