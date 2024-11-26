using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Features.Sports.Queries.GetSports;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UnitTests.Sports.Queries;

public class GetSportsTests
{
    private readonly GetSportsQuery _query = new();
    private readonly GetSportsQueryHandler _handler;
    private readonly ISportsRepository _sportsRepositoryMock;

    public GetSportsTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();
        
        _sportsRepositoryMock = Substitute.For<ISportsRepository>();
        _handler = new GetSportsQueryHandler(_sportsRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnListOfSports()
    {
        // Arrange
        var sports= new List<Sport>
        {
            Sport.Create("Football", "Description"),
            Sport.Create("Basketball", "Description")
        };
        _sportsRepositoryMock.GetAllSportsAsync().Returns(sports);

        // Act
        var result = await _handler.Handle(_query, default);
        
        // Assert
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("Football");
        result[1].Name.Should().Be("Basketball");
        result.Should().BeOfType<List<SportResponse>>();
    }
    
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoSports()
    {
        // Arrange
        _sportsRepositoryMock.GetAllSportsAsync().Returns(new List<Sport>());

        // Act
        var result = await _handler.Handle(_query, default);
        
        // Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<SportResponse>>();
    }
}