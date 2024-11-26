using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Application.Features.Buddies.Queries.GetUserBuddies;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Buddies.Queries;

public class GetUserBuddiesTests
{
    private readonly GetUserBuddiesQuery _query = new(Guid.NewGuid());
    private readonly GetUserBuddiesQueryHandler _handler;
    private readonly IBuddiesRepository _buddiesRepositoryMock;
    
    public GetUserBuddiesTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BuddyMappingProfile>();
            cfg.AddProfile<ConversationMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _buddiesRepositoryMock = Substitute.For<IBuddiesRepository>();
        _handler = new GetUserBuddiesQueryHandler(_buddiesRepositoryMock, mapper);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnListOfBuddies()
    {
        // Arrange
        var user1 = User.Create(Guid.NewGuid());
        var user2 = User.Create(Guid.NewGuid());
        var user3 = User.Create(Guid.NewGuid());

        var (buddy1, buddy2) = Buddy.CreatePair(user1.Id, user2.Id, DateTime.UtcNow);
        var (buddy3, buddy4) = Buddy.CreatePair(user1.Id, user3.Id, DateTime.UtcNow);

        var buddies = new List<Buddy> { buddy1, buddy2, buddy3, buddy4 };

        _buddiesRepositoryMock.GetUserBuddiesAsync(_query.UserId).Returns(buddies);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<BuddyResponse>>();
    }
}