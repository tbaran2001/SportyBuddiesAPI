using Moq;
using Shouldly;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Sports.Commands.DeleteSport;
using SportyBuddies.Application.UnitTests.Mocks;

namespace SportyBuddies.Application.UnitTests.Sports.Commands;

public class DeleteSportCommandHanlderTests
{
    private readonly DeleteSportCommand _deleteSportCommand;
    private readonly DeleteSportCommandHandler _handler;
    private readonly Mock<ISportsRepository> _mockSportsRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public DeleteSportCommandHanlderTests()
    {
        _mockSportsRepository = RepositoryMocks.GetSportsRepository();
        _unitOfWork = new Mock<IUnitOfWork>();

        _deleteSportCommand = new DeleteSportCommand(_mockSportsRepository.Object.GetAllAsync().Result.First().Id);
        _handler = new DeleteSportCommandHandler(_mockSportsRepository.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task DeleteSportTest()
    {
        await _handler.Handle(_deleteSportCommand, CancellationToken.None);

        var sports = await _mockSportsRepository.Object.GetAllAsync();

        sports.Count().ShouldBe(2);
    }

    [Fact]
    public async Task DeleteSportValidationTest()
    {
        var deleteSportCommand = new DeleteSportCommand(Guid.NewGuid());
        var exception = await Should.ThrowAsync<NotFoundException>(async () =>
            await _handler.Handle(deleteSportCommand, CancellationToken.None));

        var sports = await _mockSportsRepository.Object.GetAllAsync();

        sports.Count().ShouldBe(3);
        exception.ShouldNotBeNull();
    }
}