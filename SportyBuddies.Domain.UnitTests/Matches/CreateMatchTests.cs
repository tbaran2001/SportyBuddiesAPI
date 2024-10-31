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
        var match = Match.Create(user, matchedUser, matchDateTime);
        
        // Assert
        match.User.Should().Be(user);
        match.UserId.Should().Be(user.Id);
        match.MatchedUser.Should().Be(matchedUser);
        match.MatchedUserId.Should().Be(matchedUser.Id);
        match.MatchDateTime.Should().Be(matchDateTime);
        match.Swipe.Should().BeNull();
        match.SwipeDateTime.Should().BeNull();
    }
}