using System.Net;
using FluentAssertions;
using SportyBuddies.Api.IntegrationTests.Helpers;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers.SportsController;

public class DeleteSportTests:IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly SportyBuddiesWebApplicationFactory<IApiMarker> _appFactory;
    private readonly HttpClient _httpClient;
    private SportyBuddiesDbContext _dbContext;
    
    public DeleteSportTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
    }
    
    [Fact]
    public async Task DeleteSport_ReturnsNoContent_WhenSportIsDeleted()
    {
        var sport = new Sport("Football", "Football description", new List<User>());
        _dbContext.Sports.Add(sport);
        await _dbContext.SaveChangesAsync();
        
        var response = await _httpClient.DeleteAsync($"/api/sports/{sport.Id}");
        
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task DeleteSport_ReturnsNotFound_WhenSportDoesNotExist()
    {
        var response = await _httpClient.DeleteAsync($"/api/sports/{Guid.NewGuid()}");
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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