using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Application.Features.Buddies.Queries.GetProfileBuddies;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.UnitTests.Buddies.Queries;

public class GetProfileBuddiesTests
{
    private readonly GetProfileBuddiesQuery _query = new();
    private readonly GetProfileBuddiesQueryHandler _handler;
    private readonly IBuddiesRepository _buddiesRepositoryMock;
    private readonly IUserContext _userContextMock;
    
    public GetProfileBuddiesTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BuddyMappingProfile>();
            cfg.AddProfile<ConversationMappingProfile>();
            cfg.AddProfile<ProfileMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _buddiesRepositoryMock = Substitute.For<IBuddiesRepository>();
        _userContextMock = Substitute.For<IUserContext>();
        _handler = new GetProfileBuddiesQueryHandler(_buddiesRepositoryMock, mapper, _userContextMock);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnListOfBuddies()
    {
        // Arrange
        var user1 = Profile.Create(Guid.NewGuid());
        var user2 = Profile.Create(Guid.NewGuid());
        var user3 = Profile.Create(Guid.NewGuid());

        var (buddy1, buddy2) = Buddy.CreatePair(user1.Id, user2.Id, DateTime.UtcNow);
        var (buddy3, buddy4) = Buddy.CreatePair(user1.Id, user3.Id, DateTime.UtcNow);

        var buddies = new List<Buddy> { buddy1, buddy2, buddy3, buddy4 };

        var currentUser = new CurrentUser(user1.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _buddiesRepositoryMock.GetProfileBuddiesAsync(currentUser.Id).Returns(buddies);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<BuddyResponse>>();
    }
}