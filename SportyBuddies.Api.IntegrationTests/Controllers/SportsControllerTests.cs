using System.Net;
using FluentAssertions;

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
        var response = await _httpClient.GetAsync($"/api/sports/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}