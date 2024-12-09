using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Infrastructure;

namespace SportyBuddies.Application.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly SportyBuddiesDbContext DbContext;
    protected readonly Guid CurrentUserId;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<SportyBuddiesDbContext>();

        var userContextMock = _scope.ServiceProvider.GetRequiredService<IUserContext>();

        var currentUser = new CurrentUser(Guid.NewGuid(), "", []);
        userContextMock.GetCurrentUser().Returns(currentUser);

        CurrentUserId = currentUser.Id;
    }
}