using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Api.FunctionalTests.Users;

public class UpdateUserPreferencesTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateUserPreferences_ShouldReturn200Ok_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();

        var request = new UpdateUserPreferencesRequest(33,19,50,Gender.Unknown);

        // Act
        var response = await HttpClient.PutAsJsonAsync($"api/users/preferences", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}