using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.Contracts.Profiles;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Api.FunctionalTests.Profiles;

public class UpdateCurrentProfileTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateCurrentUser_ShouldReturn200Ok_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        DbContext.Profiles.Add(user);
        await DbContext.SaveChangesAsync();

        var request = new UpdateProfileRequest("Name", "Description", Gender.Female, new DateOnly(2000, 1, 1));

        // Act
        var response = await HttpClient.PutAsJsonAsync($"api/profiles", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


}