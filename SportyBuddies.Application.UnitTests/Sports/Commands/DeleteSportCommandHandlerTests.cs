using ErrorOr;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Sports.Commands.DeleteSport;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Sports.Commands;

public class DeleteSportCommandHandlerTests
{
    private readonly DeleteSportCommand _createSportCommand;
    private readonly ISportsRepository _sportsRepository = Substitute.For<ISportsRepository>();
    private readonly DeleteSportCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public DeleteSportCommandHandlerTests()
    {
        _createSportCommand = new DeleteSportCommand(Guid.NewGuid());
        _sut = new DeleteSportCommandHandler(_sportsRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldRemoveSportAndCommit_WhenRequestIsValid()
    {
        // Arrange
        var sport = new Sport("Football", "Football description", new List<User>());
        _sportsRepository.GetByIdAsync(_createSportCommand.SportId).Returns(sport);

        // Act
        var result = await _sut.Handle(_createSportCommand, CancellationToken.None);

        // Assert
        _sportsRepository.Received(1).Remove(sport);
        await _unitOfWork.Received(1).CommitChangesAsync();

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Deleted>();
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSportDoesNotExist()
    {
        // Arrange
        _sportsRepository.GetByIdAsync(_createSportCommand.SportId).Returns((Sport)null!);

        // Act
        var result = await _sut.Handle(_createSportCommand, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
    }
}