using System.Net.Http.Json;
using FluentAssertions;
using SportyBuddies.Api.FunctionalTests.Infrastructure;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Api.FunctionalTests.Sports;

public class GetSportsTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task GetSports_ShouldReturnSports_WhenValidRequest()
    {
        // Arrange
        DbContext.Sports.RemoveRange(DbContext.Sports);
        await DbContext.SaveChangesAsync();

        var sport1=Sport.Create("Football", "Football description");
        var sport2=Sport.Create("Basketball", "Basketball description");
        DbContext.Sports.Add(sport1);
        DbContext.Sports.Add(sport2);
        await DbContext.SaveChangesAsync();

        // Act
        var sports = await HttpClient.GetFromJsonAsync<List<SportResponse>>("api/sports/all");

        // Assert
        sports.Should().NotBeNull();
        sports.Should().NotBeEmpty();
        sports.Should().HaveCount(2);
        sports.Should().Contain(s => s.Name == "Football");
        sports.Should().Contain(s => s.Name == "Basketball");
    }
}