using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity.Data;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Api.FunctionalTests.Users;

public class GetCurrentUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Get_ShouldReturnUser_WhenAccessTokenIsNotMissing()
    {
        // Act
        var user = await HttpClient.GetFromJsonAsync<UserWithSportsResponse>("api/users");

        // Assert
        user.Should().NotBeNull();
        user.Id.Should().Be(UserData.TestUserId);
        user.Preferences.Should().NotBeNull();
        user.Sports.Should().NotBeNull();
        user.Gender.Should().BeDefined();
    }
}
