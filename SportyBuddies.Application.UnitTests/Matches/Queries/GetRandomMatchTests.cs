using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Matches.Queries;

public class GetRandomMatchTests
{
    private readonly GetRandomMatchQuery _query = new(Guid.NewGuid());
    private readonly GetRandomMatchQueryHandler _handler;
    private readonly IMatchesRepository _matchesRepositoryMock;

    public GetRandomMatchTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MatchMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _matchesRepositoryMock = Substitute.For<IMatchesRepository>();
        _handler = new GetRandomMatchQueryHandler(_matchesRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnRandomMatch()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var match = Match.Create(user, matchedUser, DateTime.UtcNow);
        _matchesRepositoryMock.GetRandomMatchAsync(_query.UserId).Returns(match);

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
        _matchesRepositoryMock.GetRandomMatchAsync(_query.UserId).ReturnsNull();

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeNull();
    }
}