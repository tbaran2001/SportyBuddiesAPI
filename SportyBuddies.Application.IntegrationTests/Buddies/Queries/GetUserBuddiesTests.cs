using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Application.Features.Buddies.Queries.GetUserBuddies;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Buddies.Queries;

public class GetUserBuddiesTests : BaseIntegrationTest
{
    public GetUserBuddiesTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetUserBuddies_ShouldReturnBuddies_WhenUserHasBuddies()
    {
        // Arrange
        var users = new List<User>
        {
            User.Create(Guid.NewGuid()),
            User.Create(Guid.NewGuid()),
            User.Create(Guid.NewGuid())
        };
        var buddies = new List<Buddy>
        {
            Buddy.Create(users[0], users[1], DateTime.UtcNow),
            Buddy.Create(users[0], users[2], DateTime.UtcNow)
        };
        await DbContext.Users.AddRangeAsync(users);
        await DbContext.Buddies.AddRangeAsync(buddies);
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserBuddiesQuery(users[0].Id);
        
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
        var user = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserBuddiesQuery(user.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().BeEmpty();
    }
}