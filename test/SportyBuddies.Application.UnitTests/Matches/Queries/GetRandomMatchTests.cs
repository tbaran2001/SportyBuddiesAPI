using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Matches.Queries;

public class GetRandomMatchTests
{
    private readonly GetRandomMatchQuery _query = new();
    private readonly GetRandomMatchQueryHandler _handler;
    private readonly IMatchesRepository _matchesRepositoryMock;
    private readonly IUserContext _userContextMock;

    public GetRandomMatchTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MatchMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _matchesRepositoryMock = Substitute.For<IMatchesRepository>();
        _userContextMock = Substitute.For<IUserContext>();
        _handler = new GetRandomMatchQueryHandler(_matchesRepositoryMock, mapper, _userContextMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnRandomMatch()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var (match1, match2) = Match.CreatePair(user.Id, matchedUser.Id, DateTime.UtcNow);

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _matchesRepositoryMock.GetRandomMatchAsync(currentUser.Id).Returns(match1);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<RandomMatchResponse>();
    }

    [Fact]
    public async Task Handle_Should_ReturnNull_WhenMatchNotFound()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _matchesRepositoryMock.GetRandomMatchAsync(Guid.NewGuid()).ReturnsNull();

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeNull();
    }
}