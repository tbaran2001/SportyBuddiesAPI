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
        var (buddy1,buddy2) = Buddy.CreatePair(user.Id, matchedUser.Id, matchDateTime);
        
        // Assert
        buddy1.UserId.Should().Be(user.Id);
        buddy1.MatchedUserId.Should().Be(matchedUser.Id);
        buddy1.CreatedOnUtc.Should().Be(matchDateTime);
        buddy1.OppositeBuddyId.Should().Be(buddy2.Id);
        buddy1.ConversationId.Should().BeNull();

        buddy2.UserId.Should().Be(matchedUser.Id);
        buddy2.MatchedUserId.Should().Be(user.Id);
        buddy2.CreatedOnUtc.Should().Be(matchDateTime);
        buddy2.OppositeBuddyId.Should().Be(buddy1.Id);
        buddy2.ConversationId.Should().BeNull();
    }
}