using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;
using Gender = SportyBuddies.Api.Contracts.Users.Gender;

namespace SportyBuddies.Api.IntegrationTests.Controllers.CurrentUserController;

public class UpdateCurrentUserTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly HttpClient _httpClient = appFactory.CreateClient();
    private SportyBuddiesDbContext _dbContext;

    public async Task InitializeAsync()
    {
        _dbContext = await DatabaseHelper.InitializeDatabaseAsync(appFactory);
    }

    public async Task DisposeAsync()
    {
        _dbContext.Users.RemoveRange(_dbContext.Users);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateCurrentUser_ReturnsUser_WhenUserIsAuthenticated()
    {
        var user = new User("John", null, DateTime.Now, new List<Sport>(), null,
            new List<UserPhoto>(), null);
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        await AuthHelper.AuthenticateUserAsync(_httpClient, appFactory.Services, "John", "Password123!");

        var request = new UpdateUserRequest("John", "", Gender.Male, DateTime.Now);

        var response = await _httpClient.PutAsJsonAsync("/api/currentuser", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}