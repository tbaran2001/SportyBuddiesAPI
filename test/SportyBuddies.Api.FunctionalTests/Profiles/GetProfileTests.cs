using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Api.FunctionalTests.Profiles;

public class GetProfileTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task GetUser_ShouldReturnUser_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        DbContext.Profiles.Add(user);
        await DbContext.SaveChangesAsync();

        // Act
        var userResponse = await HttpClient.GetFromJsonAsync<ProfileWithSportsResponse>($"api/profiles/{user.Id}");

        // Assert
        userResponse.Should().NotBeNull();
        userResponse.Id.Should().Be(user.Id);
        userResponse.Preferences.Should().NotBeNull();
        userResponse.Sports.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUser_ShouldReturn200Ok_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        DbContext.Profiles.Add(user);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await HttpClient.GetAsync($"api/profiles/{user.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

}