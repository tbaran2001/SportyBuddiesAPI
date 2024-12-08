using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Api.FunctionalTests.Users;

public class GetUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task GetUser_ShouldReturnUser_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();

        // Act
        var userResponse = await HttpClient.GetFromJsonAsync<UserWithSportsResponse>($"api/users/{user.Id}");

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
        var user = User.Create(Guid.NewGuid());
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await HttpClient.GetAsync($"api/users/{user.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

}