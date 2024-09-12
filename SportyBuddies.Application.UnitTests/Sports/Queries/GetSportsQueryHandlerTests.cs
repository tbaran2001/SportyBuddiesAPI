using AutoMapper;
using Moq;
using Shouldly;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Sports.Queries.GetSports;
using SportyBuddies.Application.UnitTests.Mocks;

namespace SportyBuddies.Application.UnitTests.Sports.Queries;

public class GetSportsQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ISportsRepository> _mockSportsRepository;

    public GetSportsQueryHandlerTests()
    {
        _mockSportsRepository = RepositoryMocks.GetSportsRepository();
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task GetSportsTest()
    {
        var handler = new GetSportsQueryHandler(_mockSportsRepository.Object, _mapper);
        var result = await handler.Handle(new GetSportsQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<SportDto>>();
        result.Count().ShouldBe(3);
    }
}