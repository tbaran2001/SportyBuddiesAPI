using System.Net;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Contracts.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.UsersController;

public class CreateUserTests : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly SportyBuddiesWebApplicationFactory<IApiMarker> _appFactory;
    private readonly HttpClient _httpClient;
    private SportyBuddiesDbContext _dbContext;

    public CreateUserTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
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

    [Fact]
    public async Task CreateUser_ReturnsUser_WhenUserIsCreated()
    {
        var request = new CreateUserRequest("John", "Doe");
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/users", content);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain("John");
    }

    [Fact]
    public async Task CreateUser_ReturnsBadRequest_WhenFirstNameIsMissing()
    {
        var request = new CreateUserRequest("", "Doe");
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/users", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}