﻿using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Sports.Queries.GetSports;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Sports.Queries;

public class GetSportsQueryHandlerTests
{
    private readonly GetSportsQuery _getSportQuery;
    private readonly IMapper _mapper;
    private readonly ISportsRepository _sportsRepository = Substitute.For<ISportsRepository>();
    private readonly GetSportsQueryHandler _sut;

    public GetSportsQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });

        _mapper = configurationProvider.CreateMapper();
        _getSportQuery = new GetSportsQuery();
        _sut = new GetSportsQueryHandler(_sportsRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfSports_WhenRequestIsValid()
    {
        // Arrange
        var sports = new List<Sport>
        {
            new("Football", "Football description", new List<User>()),
            new("Basketball", "Basketball description", new List<User>())
        };
        _sportsRepository.GetAllSportsAsync().Returns(sports);

        // Act
        var result = await _sut.Handle(_getSportQuery, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Name == "Football");
        result.Should().Contain(x => x.Name == "Basketball");
    }
}