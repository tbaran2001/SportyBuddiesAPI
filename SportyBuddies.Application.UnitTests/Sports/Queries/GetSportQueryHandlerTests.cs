﻿using AutoMapper;
using ErrorOr;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Sports.Queries.GetSport;
using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Application.UnitTests.Sports.Queries;

public class GetSportQueryHandlerTests
{
    private readonly GetSportQuery _getSportQuery;
    private readonly IMapper _mapper;
    private readonly ISportsRepository _sportsRepository = Substitute.For<ISportsRepository>();
    private readonly GetSportQueryHandler _sut;

    public GetSportQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });

        _mapper = configurationProvider.CreateMapper();
        _getSportQuery = new GetSportQuery(SportId.CreateUnique());
        _sut = new GetSportQueryHandler(_sportsRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnSport_WhenRequestIsValid()
    {
        // Arrange
        var sport = Sport.Create("Football", "Football description");
        _sportsRepository.GetSportByIdAsync(_getSportQuery.SportId).Returns(sport);

        // Act
        var result = await _sut.Handle(_getSportQuery, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Name.Should().Be("Football");
        result.Value.Description.Should().Be("Football description");
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSportDoesNotExist()
    {
        // Arrange
        _sportsRepository.GetSportByIdAsync(_getSportQuery.SportId).Returns((Sport)null!);

        // Act
        var result = await _sut.Handle(_getSportQuery, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
    }
}