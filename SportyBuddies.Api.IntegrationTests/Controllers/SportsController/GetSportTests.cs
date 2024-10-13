using System.Net;
using FluentAssertions;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.SportsController;

public class GetSportTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
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
        _dbContext.Sports.RemoveRange(_dbContext.Sports);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetSport_ReturnsNotFound_WhenSportDoesNotExist()
    {
        var response = await _httpClient.GetAsync($"/api/sports/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSport_ReturnsSport_WhenSportExists()
    {
        var sport = Sport.Create("Football", "Football description");
        _dbContext.Sports.Add(sport);
        await _dbContext.SaveChangesAsync();

        var response = await _httpClient.GetAsync($"/api/sports/{sport.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Football");
    }
}