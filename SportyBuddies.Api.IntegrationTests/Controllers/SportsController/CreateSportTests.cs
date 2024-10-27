using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.Contracts.Sports;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Infrastructure;

namespace SportyBuddies.Api.IntegrationTests.Controllers.SportsController;

public class CreateSportTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly HttpClient _httpClient = appFactory.CreateClient();
    private SportyBuddiesDbContext _dbContext;

    [Fact]
    public async Task CreateSport_ReturnsSport_WhenSportIsCreated()
    {
        var request = new CreateSportRequest("Football", "Football description");
        
        var response = await _httpClient.PostAsJsonAsync("/api/sports", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Football");
    }
    
    [Fact]
    public async Task CreateSport_ReturnsBadRequest_WhenSportNameIsMissing()
    {
        var request = new CreateSportRequest("", "Football description");
        
        var response = await _httpClient.PostAsJsonAsync("/api/sports", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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