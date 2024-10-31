using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Sports.Queries.GetSport;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UnitTests.Sports.Queries;

public class GetSportTests
{
    private readonly GetSportQuery _query = new(Guid.NewGuid());
    private readonly GetSportQueryHandler _handler;
    private readonly ISportsRepository _sportsRepositoryMock;

    public GetSportTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();
        
        _sportsRepositoryMock = Substitute.For<ISportsRepository>();
        _handler = new GetSportQueryHandler(_sportsRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnSport_WhenRequestIsValid()
    {
        // Arrange
        var sport = Sport.Create("Football", "Football description");
        _sportsRepositoryMock.GetSportByIdAsync(_query.SportId).Returns(sport);

        // Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        result.Name.Should().Be("Football");
        result.Description.Should().Be("Football description");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenSportDoesNotExists()
    {
        // Arrange
        _sportsRepositoryMock.GetSportByIdAsync(_query.SportId).Returns((Sport?)null);

        // Act
        var act = () => _handler.Handle(_query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}