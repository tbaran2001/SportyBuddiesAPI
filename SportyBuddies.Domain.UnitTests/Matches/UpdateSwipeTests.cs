using FluentAssertions;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Matches;

public class UpdateSwipeTests
{
    [Fact]
    public void UpdateSwipe_Should_SetSwipeAndSwipeDateTime()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var matchDateTime = DateTime.UtcNow;
        var match = Match.Create(user, matchedUser, matchDateTime);
        var swipe = Swipe.Left;

        // Act
        match.UpdateSwipe(swipe);

        // Assert
        match.Swipe.Should().Be(swipe);
        match.SwipeDateTime.Should().NotBeNull();
    }
}