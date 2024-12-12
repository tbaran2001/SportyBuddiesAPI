using FluentAssertions;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.UnitTests.Matches;

public class CreateMatchTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        var matchedUser = Profile.Create(Guid.NewGuid());
        var matchDateTime = DateTime.UtcNow;
        
        // Act
        var (match1, match2) = Match.CreatePair(user.Id, matchedUser.Id, matchDateTime);

        // Assert
        match1.ProfileId.Should().Be(user.Id);
        match1.MatchedProfileId.Should().Be(matchedUser.Id);
        match1.MatchDateTime.Should().Be(matchDateTime);
        match1.OppositeMatchId.Should().Be(match2.Id);
        match1.Swipe.Should().BeNull();
        match1.SwipeDateTime.Should().BeNull();

        match2.ProfileId.Should().Be(matchedUser.Id);
        match2.MatchedProfileId.Should().Be(user.Id);
        match2.MatchDateTime.Should().Be(matchDateTime);
        match2.OppositeMatchId.Should().Be(match1.Id);
        match2.Swipe.Should().BeNull();
        match2.SwipeDateTime.Should().BeNull();
    }
}