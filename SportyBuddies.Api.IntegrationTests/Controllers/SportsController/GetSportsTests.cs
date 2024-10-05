using System.Net;
using FluentAssertions;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.SportsController;

public class GetSportsTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly HttpClient _httpClient = appFactory.CreateClient();
    private SportyBuddiesDbContext _dbContext;

    [Fact]
    public async Task GetSports_ReturnsSports()
    {
        var sport1 = new Sport("Football", "Football description", new List<User>());
        var sport2 = new Sport("Basketball", "Basketball description", new List<User>());
        _dbContext.Sports.AddRange(sport1, sport2);
        await _dbContext.SaveChangesAsync();

        var response = await _httpClient.GetAsync("/api/sports");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Football");
        content.Should().Contain("Basketball");
    }
    
    [Fact]
    public async Task GetSports_ReturnsEmptyList_WhenNoSports()
    {
        var response = await _httpClient.GetAsync("/api/sports");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("[]");
    }

    public async Task InitializeAsync()
    {
        _dbContext = await DatabaseHelper.InitializeDatabaseAsync(appFactory);
    }

    public async Task DisposeAsync()
    {
        _dbContext.Sports.RemoveRange(_dbContext.Sports);
        await _dbContext.SaveChangesAsync();
    }
}