using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.Contracts.Profiles;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Api.FunctionalTests.Profiles;

public class UpdateProfilePreferencesTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateUserPreferences_ShouldReturn200Ok_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        DbContext.Profiles.Add(user);
        await DbContext.SaveChangesAsync();

        var request = new UpdateProfilePreferencesRequest(33,19,50,Gender.Unknown);

        // Act
        var response = await HttpClient.PutAsJsonAsync($"api/profiles/preferences", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}