using System.Net;
using FluentAssertions;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.SportsController;

public class DeleteSportTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
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
    public async Task DeleteSport_ReturnsNoContent_WhenSportIsDeleted()
    {
        var sport = Sport.Create("Football", "Football description");
        _dbContext.Sports.Add(sport);
        await _dbContext.SaveChangesAsync();

        var response = await _httpClient.DeleteAsync($"/api/sports/{sport.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteSport_ReturnsNotFound_WhenSportDoesNotExist()
    {
        var response = await _httpClient.DeleteAsync($"/api/sports/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}