using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Application.Matches.Queries.GetUserMatches;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Matches.Queries;

public class GetUserMatchesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUserMatches_ShouldReturnMatches_WhenMatchesExist()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser1 = User.Create(Guid.NewGuid());
        var matchedUser2 = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user);
        await DbContext.Users.AddAsync(matchedUser1);

        var match1 = Match.Create(user, matchedUser1, DateTime.UtcNow);
        var match2 = Match.Create(user, matchedUser2, DateTime.UtcNow);
        await DbContext.Matches.AddAsync(match1);
        await DbContext.Matches.AddAsync(match2);

        await DbContext.SaveChangesAsync();

        var query = new GetUserMatchesQuery(user.Id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeOfType<List<MatchResponse>>();
        result.Should().NotBeNull();
        result.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task GetUserMatches_ShouldReturnEmptyList_WhenMatchesDoNotExist()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserMatchesQuery(user.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().BeEmpty();
    }
}