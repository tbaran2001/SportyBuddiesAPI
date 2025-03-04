﻿using FluentAssertions;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.UnitTests.Buddies;

public class CreateBuddyTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        var matchedUser = Profile.Create(Guid.NewGuid());
        var matchDateTime = DateTime.UtcNow;
        
        // Act
        var (buddy1,buddy2) = Buddy.CreatePair(user.Id, matchedUser.Id, matchDateTime);
        
        // Assert
        buddy1.ProfileId.Should().Be(user.Id);
        buddy1.MatchedProfileId.Should().Be(matchedUser.Id);
        buddy1.CreatedOnUtc.Should().Be(matchDateTime);
        buddy1.OppositeBuddyId.Should().Be(buddy2.Id);
        buddy1.ConversationId.Should().BeNull();

        buddy2.ProfileId.Should().Be(matchedUser.Id);
        buddy2.MatchedProfileId.Should().Be(user.Id);
        buddy2.CreatedOnUtc.Should().Be(matchDateTime);
        buddy2.OppositeBuddyId.Should().Be(buddy1.Id);
        buddy2.ConversationId.Should().BeNull();
    }
}