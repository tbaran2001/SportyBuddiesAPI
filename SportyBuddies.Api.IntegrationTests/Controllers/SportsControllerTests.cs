using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Api.IntegrationTests.Controllers;

public class SportsControllerTests : IClassFixture<SportyBuddiesWebApplicationFactory<IApiMarker>>
{
    private readonly SportyBuddiesWebApplicationFactory<IApiMarker> _appFactory;
    private readonly HttpClient _httpClient;

    public SportsControllerTests(SportyBuddiesWebApplicationFactory<IApiMarker> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task GetSport_ReturnsNotFound_WhenSportDoesNotExist()
    {
        using var scope = _appFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<SportyBuddiesDbContext>();

        await db.Database.EnsureCreatedAsync();

        var response = await _httpClient.GetAsync($"/api/sports/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSport_ReturnsSport_WhenSportExists()
    {
        using var scope = _appFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<SportyBuddiesDbContext>();

        await db.Database.EnsureCreatedAsync();

        var sport = new Sport("Football", "Football description", new List<User>());
        db.Sports.Add(sport);
        await db.SaveChangesAsync();

        var response = await _httpClient.GetAsync($"/api/sports/{sport.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Football");
    }
}