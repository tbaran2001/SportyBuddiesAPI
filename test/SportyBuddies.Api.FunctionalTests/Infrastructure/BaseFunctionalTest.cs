using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Infrastructure;

namespace SportyBuddies.Api.FunctionalTests.Infrastructure;

public class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient;
    private readonly IServiceScope _scope;
    protected readonly SportyBuddiesDbContext DbContext;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();

        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<SportyBuddiesDbContext>();
    }
}