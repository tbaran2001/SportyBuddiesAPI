using FluentAssertions;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Matches;

public class CreateMatchTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var matchDateTime = DateTime.UtcNow;
        
        // Act
        var (match1, match2) = Match.CreatePair(user.Id, matchedUser.Id, matchDateTime);

        // Assert
        match1.UserId.Should().Be(user.Id);
        match1.MatchedUserId.Should().Be(matchedUser.Id);
        match1.MatchDateTime.Should().Be(matchDateTime);
        match1.OppositeMatchId.Should().Be(match2.Id);
        match1.Swipe.Should().BeNull();
        match1.SwipeDateTime.Should().BeNull();

        match2.UserId.Should().Be(matchedUser.Id);
        match2.MatchedUserId.Should().Be(user.Id);
        match2.MatchDateTime.Should().Be(matchDateTime);
        match2.OppositeMatchId.Should().Be(match1.Id);
        match2.Swipe.Should().BeNull();
        match2.SwipeDateTime.Should().BeNull();
    }
}