﻿using FluentAssertions;
using NSubstitute;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Services;

namespace SportyBuddies.Domain.UnitTests.Services.Buddies;

public class AddBuddyTests
{
    private readonly IBuddiesRepository _buddiesRepository;
    private readonly BuddyService _buddyService;

    public AddBuddyTests()
    {
        _buddiesRepository = Substitute.For<IBuddiesRepository>();
        _buddyService = new BuddyService(_buddiesRepository);
    }

    [Fact]
    public async Task AddBuddyAsync_WhenSwipesAreNotRight_ShouldNotAddBuddies()
    {
        // Arrange
        var (match1, match2) = Match.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        match1.UpdateSwipe(Swipe.Left);

        // Act
        await _buddyService.AddBuddyAsync(match1, match2);

        // Assert
        await _buddiesRepository.DidNotReceive().AddBuddyAsync(Arg.Any<Buddy>());
    }

    [Fact]
    public async Task AddBuddyAsync_WhenUsersAreAlreadyBuddies_ShouldThrowException()
    {
        var (match1, match2) = Match.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        _buddiesRepository
            .AreProfilesAlreadyBuddiesAsync(match1.ProfileId, match2.ProfileId)
            .Returns(true);

        // Act
        var act = async () => await _buddyService.AddBuddyAsync(match1, match2);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Profiles are already buddies");
    }

    [Fact]
    public async Task AddBuddyAsync_WhenMatchAndOppositeMatchAreTheSame_ShouldThrowException()
    {
        var (match1, match2) = Match.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        // Act
        var act = async () => await _buddyService.AddBuddyAsync(match1, match1);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Match and opposite match cannot be the same");
    }

    [Fact]
    public async Task AddBuddyAsync_WhenSwipesAreRight_ShouldAddBuddiesWithCorrectData()
    {
        // Arrange
        var (match1, match2) = Match.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        match1.UpdateSwipe(Swipe.Right);
        match2.UpdateSwipe(Swipe.Right);

        // Act
        await _buddyService.AddBuddyAsync(match1, match2);

        // Assert
        await _buddiesRepository.Received(1).AddBuddyAsync(Arg.Is<Buddy>(buddy =>
            buddy.ProfileId == match1.ProfileId &&
            buddy.MatchedProfileId == match2.ProfileId));
        await _buddiesRepository.Received(1).AddBuddyAsync(Arg.Is<Buddy>(buddy =>
            buddy.ProfileId == match2.ProfileId &&
            buddy.MatchedProfileId == match1.ProfileId));
    }
}