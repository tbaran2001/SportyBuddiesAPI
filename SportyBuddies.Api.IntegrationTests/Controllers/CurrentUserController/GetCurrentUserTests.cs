using System.Net;
using FluentAssertions;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.CurrentUserController;

public class GetCurrentUserTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly HttpClient _httpClient = appFactory.CreateClient();
    private SportyBuddiesDbContext _dbContext;

    [Fact]
    public async Task GetCurrentUser_ReturnsUser_WhenUserIsAuthenticated()
    {
        var user = new User("John", "Doe", DateTime.Now, new List<Sport>());
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        
        await AuthHelper.AuthenticateUserAsync(_httpClient, appFactory.Services, "John", "Password123!");
        
        var response = await _httpClient.GetAsync("/api/currentuser");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain("John");
    }
    
    public async Task InitializeAsync()
    {
        _dbContext = await DatabaseHelper.InitializeDatabaseAsync(appFactory);
    }
    
    public async Task DisposeAsync()
    {
        _dbContext.Users.RemoveRange(_dbContext.Users);
        await _dbContext.SaveChangesAsync();
    }
}