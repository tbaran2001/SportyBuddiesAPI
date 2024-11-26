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
        var (match1, _) = Match.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        var swipe = Swipe.Left;

        // Act
        match1.UpdateSwipe(swipe);

        // Assert
        match1.Swipe.Should().Be(swipe);
        match1.SwipeDateTime.Should().NotBeNull();
    }
}