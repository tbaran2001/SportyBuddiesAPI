using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Api.FunctionalTests.Users;

public class UpdateCurrentUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateCurrentUser_ShouldReturn200Ok_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();

        var request = new UpdateUserRequest("Name", "Description", Gender.Female, new DateOnly(2000, 1, 1));

        // Act
        var response = await HttpClient.PutAsJsonAsync($"api/users", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


}