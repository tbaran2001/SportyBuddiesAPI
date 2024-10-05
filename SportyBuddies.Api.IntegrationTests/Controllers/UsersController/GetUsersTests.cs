using System.Net;
using FluentAssertions;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.UsersController;

public class GetUsersTests : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly SportyBuddiesWebApplicationFactory<IApiMarker> _appFactory;
    private readonly HttpClient _httpClient;
    private SportyBuddiesDbContext _dbContext;

    public GetUsersTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
    }
    
    [Fact]
    public async Task GetUsers_ReturnsEmptyList_WhenNoUsersExist()
    {
        var response = await _httpClient.GetAsync("/api/users");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("[]");
    }
    
    [Fact]
    public async Task GetUsers_ReturnsUsers_WhenUsersExist()
    {
        var user1 = new User("John", "Doe", DateTime.Now, new List<Sport>());
        var user2 = new User("Jane", "Doe", DateTime.Now, new List<Sport>());
        _dbContext.Users.AddRange(user1, user2);
        await _dbContext.SaveChangesAsync();
        
        var response = await _httpClient.GetAsync("/api/users");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("John");
        content.Should().Contain("Jane");
    }
    
    public async Task InitializeAsync()
    {
        _dbContext = await DatabaseHelper.InitializeDatabaseAsync(_appFactory);
    }
    
    public async Task DisposeAsync()
    {
        _dbContext.Users.RemoveRange(_dbContext.Users);
        await _dbContext.SaveChangesAsync();
    }

}
