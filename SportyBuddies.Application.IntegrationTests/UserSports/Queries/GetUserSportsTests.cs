using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Application.UserSports.Queries.GetUserSports;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.UserSports.Queries;

public class GetUserSportsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUserSports_ShouldReturnListOfSports()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport1 = Sport.Create("Football", "Football description");
        var sport2 = Sport.Create("Basketball", "Basketball description");
        user.AddSport(sport1);
        user.AddSport(sport2);
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserSportsQuery(user.Id);

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
        var user = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserSportsQuery(user.Id);

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
        var query = new GetUserSportsQuery(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await Sender.Send(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}