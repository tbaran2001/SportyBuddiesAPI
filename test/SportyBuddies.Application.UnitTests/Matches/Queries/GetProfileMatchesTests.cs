using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Queries.GetProfileMatches;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.UnitTests.Matches.Queries;

public class GetProfileMatchesTests
{
    private readonly GetProfileMatchesQuery _query = new(Guid.NewGuid());
    private readonly GetProfileMatchesQueryHandler _handler;
    private readonly IMatchesRepository _matchesRepositoryMock;
    
    public GetProfileMatchesTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MatchMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _matchesRepositoryMock = Substitute.For<IMatchesRepository>();
        _handler = new GetProfileMatchesQueryHandler(_matchesRepositoryMock, mapper);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnListOfMatches()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        var matchedUser = Profile.Create(Guid.NewGuid());
        var (match1, match2) = Match.CreatePair(user.Id, matchedUser.Id, DateTime.UtcNow);
        var matches = new List<Match>
        {
            match1,
            match2
        };
        _matchesRepositoryMock.GetProfileMatchesAsync(_query.ProfileId).Returns(matches);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<MatchResponse>>();
    }
}