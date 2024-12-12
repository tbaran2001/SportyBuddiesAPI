using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Application.Features.Buddies.Queries.GetProfileBuddies;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.IntegrationTests.Buddies.Queries;

public class GetProfileBuddiesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUserBuddies_ShouldReturnBuddies_WhenUserHasBuddies()
    {
        // Arrange
        var users = new List<Profile>
        {
            Profile.Create(CurrentUserId),
            Profile.Create(Guid.NewGuid()),
            Profile.Create(Guid.NewGuid())
        };
        var (buddy1, buddy2) = Buddy.CreatePair(users[0].Id, users[1].Id, DateTime.UtcNow);
        var (buddy3, buddy4) = Buddy.CreatePair(users[0].Id, users[2].Id, DateTime.UtcNow);

        var buddies = new List<Buddy>
        {
            buddy1, buddy2, buddy3, buddy4
        };
        await DbContext.Profiles.AddRangeAsync(users);
        await DbContext.Buddies.AddRangeAsync(buddies);
        await DbContext.SaveChangesAsync();

        var query = new GetProfileBuddiesQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<BuddyResponse>>();
        result.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetUserBuddies_ShouldReturnEmptyList_WhenUserDoesNotHaveBuddies()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.SaveChangesAsync();

        var query = new GetProfileBuddiesQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeEmpty();
    }
}