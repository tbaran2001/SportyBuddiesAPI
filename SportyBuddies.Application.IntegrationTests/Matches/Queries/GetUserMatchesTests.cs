using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Queries.GetUserMatches;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
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
        await DbContext.Users.AddAsync(matchedUser2);
        await DbContext.SaveChangesAsync();

        var (match1,match2) = Match.CreatePair(user.Id, matchedUser1.Id, DateTime.UtcNow);
        var (match3,match4) = Match.CreatePair(user.Id, matchedUser2.Id, DateTime.UtcNow);

        await DbContext.Matches.AddRangeAsync(match1, match2, match3, match4);
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