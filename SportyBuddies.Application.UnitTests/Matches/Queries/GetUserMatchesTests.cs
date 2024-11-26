using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Queries.GetUserMatches;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Matches.Queries;

public class GetUserMatchesTests
{
    private readonly GetUserMatchesQuery _query = new(Guid.NewGuid());
    private readonly GetUserMatchesQueryHandler _handler;
    private readonly IMatchesRepository _matchesRepositoryMock;
    
    public GetUserMatchesTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MatchMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _matchesRepositoryMock = Substitute.For<IMatchesRepository>();
        _handler = new GetUserMatchesQueryHandler(_matchesRepositoryMock, mapper);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnListOfMatches()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var (match1, match2) = Match.CreatePair(user.Id, matchedUser.Id, DateTime.UtcNow);
        var matches = new List<Match>
        {
            match1,
            match2
        };
        _matchesRepositoryMock.GetUserMatchesAsync(_query.UserId).Returns(matches);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<MatchResponse>>();
    }
}