using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Contracts.Sports;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.SportsController;

public class CreateSportTests:IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly SportyBuddiesWebApplicationFactory<IApiMarker> _appFactory;
    private readonly HttpClient _httpClient;
    private SportyBuddiesDbContext _dbContext;
    
    public CreateSportTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
    }
    
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
        _dbContext = await DatabaseHelper.InitializeDatabaseAsync(_appFactory);
    }
    
    public async Task DisposeAsync()
    {
        _dbContext.Sports.RemoveRange(_dbContext.Sports);
        await _dbContext.SaveChangesAsync();
    }
}