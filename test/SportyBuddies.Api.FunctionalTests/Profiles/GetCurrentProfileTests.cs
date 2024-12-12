using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Api.FunctionalTests.Profiles;

public class GetCurrentProfileTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task GetCurrentUser_ShouldReturn200Ok_WhenValidRequest()
    {
        // Arrange

        // Act
        var response = await HttpClient.GetAsync("api/profiles/me");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
