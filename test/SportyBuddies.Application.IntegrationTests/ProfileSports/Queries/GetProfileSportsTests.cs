using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.ProfileSports.Queries.GetProfileSports;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.IntegrationTests.ProfileSports.Queries;

public class GetProfileSportsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUserSports_ShouldReturnListOfSports()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        var sport1 = Sport.Create("Football", "Football description");
        var sport2 = Sport.Create("Basketball", "Basketball description");
        user.AddSport(sport1);
        user.AddSport(sport2);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetProfileSportsQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Count.Should().Be(2);
        result.Should().Contain(s => s.Name == "Football");
        result.Should().Contain(s => s.Name == "Basketball");
        result.Should().BeOfType<List<SportResponse>>();
    }
    
    [Fact]
    public async Task GetUserSports_ShouldReturnEmptyList_WhenUserHasNoSports()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetProfileSportsQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Count.Should().Be(0);
        result.Should().BeOfType<List<SportResponse>>();
    }
    
    [Fact]
    public async Task GetUserSports_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var query = new GetProfileSportsQuery();

        // Act
        Func<Task> act = async () => await Sender.Send(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}