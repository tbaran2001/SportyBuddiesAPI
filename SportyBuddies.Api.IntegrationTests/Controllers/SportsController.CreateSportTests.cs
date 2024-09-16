using System.Net;
using System.Net.Http.Json;
using Shouldly;
using SportyBuddies.Api.IntegrationTests.Common;
using SportyBuddies.Contracts.Sports;

namespace SportyBuddies.Api.IntegrationTests.Controllers;

[Collection(SportyBuddiesApiCollection.CollectionName)]
public class CreateSubscriptionTests
{
    private readonly HttpClient _client;

    public CreateSubscriptionTests(SportyBuddiesApiFactory apiFactory)
    {
        _client = apiFactory.HttpClient;
        apiFactory.ResetDatabase();
    }

    [Fact]
    public async Task CreateSport_WhenValidSport_ShouldCreateSport()
    {
        // Arrange
        var createSportRequest = new CreateSportRequest(
            "Football",
            "Football description");

        // Act
        var response = await _client.PostAsJsonAsync("api/Sports", createSportRequest);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();

        var sportResponse = await response.Content.ReadFromJsonAsync<SportResponse>();
        sportResponse.ShouldNotBeNull();
        sportResponse!.Name.ShouldBe("Football");

        response.Headers.Location!.PathAndQuery.ShouldBe($"/api/Sports/{sportResponse.Id}");
    }
}