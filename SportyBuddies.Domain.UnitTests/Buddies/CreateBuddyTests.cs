using FluentAssertions;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Buddies;

public class CreateBuddyTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var matchDateTime = DateTime.UtcNow;
        
        // Act
        var buddy = Buddy.Create(user, matchedUser, matchDateTime);
        
        // Assert
        buddy.User.Should().Be(user);
        buddy.UserId.Should().Be(user.Id);
        buddy.MatchedUser.Should().Be(matchedUser);
        buddy.MatchedUserId.Should().Be(matchedUser.Id);
        buddy.MatchDateTime.Should().Be(matchDateTime);
    }
}