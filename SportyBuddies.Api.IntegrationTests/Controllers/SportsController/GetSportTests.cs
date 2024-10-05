using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;
using Xunit;
using System.Threading.Tasks;

namespace SportyBuddies.Api.IntegrationTests.Controllers.SportsController;

public class GetSportTests : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly SportyBuddiesWebApplicationFactory<IApiMarker> _appFactory;
    private readonly HttpClient _httpClient;
    private SportyBuddiesDbContext _dbContext;

    public GetSportTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
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
        var sport = new Sport("Football", "Football description", new List<User>());
        _dbContext.Sports.Add(sport);
        await _dbContext.SaveChangesAsync();

        var response = await _httpClient.GetAsync($"/api/sports/{sport.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Football");
    }
    
    public async Task InitializeAsync()
    {
        _dbContext = await DatabaseHelper.InitializeDatabaseAsync(_appFactory);
    }

    public async Task DisposeAsync()
    {
        _dbContext.Sports.RemoveRange(_dbContext.Sports);
        await _dbContext.SaveChangesAsync();
    }
}
