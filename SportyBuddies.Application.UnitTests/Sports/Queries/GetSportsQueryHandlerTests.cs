using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Sports.Queries.GetSports;
using SportyBuddies.Domain.SportAggregate;

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
            Sport.Create("Football", "Football description"),
            Sport.Create("Basketball", "Basketball description")
        };
        _sportsRepository.GetAllSportsAsync().Returns(sports);

        // Act
        var result = await _sut.Handle(_getSportQuery, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().HaveCount(2);
        result.Value.Should().Contain(x => x.Name == "Football");
        result.Value.Should().Contain(x => x.Name == "Basketball");
    }
}