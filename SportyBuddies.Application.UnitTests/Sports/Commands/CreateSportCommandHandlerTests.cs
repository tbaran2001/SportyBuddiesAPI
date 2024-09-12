using AutoMapper;
using Moq;
using Shouldly;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Application.UnitTests.Mocks;

namespace SportyBuddies.Application.UnitTests.Sports.Commands;

public class CreateSportCommandHandlerTests
{
    private readonly CreateSportCommand _createSportCommand;
    private readonly CreateSportCommandHandler _handler;
    private readonly IMapper _mapper;
    private readonly Mock<ISportsRepository> _mockSportsRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public CreateSportCommandHandlerTests()
    {
        _mockSportsRepository = RepositoryMocks.GetSportsRepository();
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });
        _mapper = configurationProvider.CreateMapper();
        _unitOfWork = new Mock<IUnitOfWork>();

        _createSportCommand = new CreateSportCommand("Volleyball", "Volleyball description");
        _handler = new CreateSportCommandHandler(_mockSportsRepository.Object, _unitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task Handle()
    {
        var result = await _handler.Handle(_createSportCommand, CancellationToken.None);

        var sports = await _mockSportsRepository.Object.GetAllAsync();

        sports.Count().ShouldBe(4);
        result.ShouldBeOfType<SportDto>();
    }

    [Fact]
    public async Task CreateSportValidationTest()
    {
        var createSportCommand = new CreateSportCommand("", "");
        var exception = await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createSportCommand, CancellationToken.None));

        var sports = await _mockSportsRepository.Object.GetAllAsync();

        sports.Count().ShouldBe(3);
        exception.ShouldNotBeNull();
        exception.Errors.Count.ShouldBe(2);
    }

    [Fact]
    public async Task CreateSportMappingTest()
    {
        var result = await _handler.Handle(_createSportCommand, CancellationToken.None);

        result.Name.ShouldBe(_createSportCommand.Name);
        result.Description.ShouldBe(_createSportCommand.Description);
    }
}