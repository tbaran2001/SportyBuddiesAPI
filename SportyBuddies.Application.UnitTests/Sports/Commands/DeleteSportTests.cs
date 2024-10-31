using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Sports.Commands.DeleteSport;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UnitTests.Sports.Commands;

public class DeleteSportTests
{
    private readonly DeleteSportCommand _command = new(Guid.NewGuid());
    private readonly DeleteSportCommandHandler _handler;
    private readonly ISportsRepository _sportsRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public DeleteSportTests()
    {
        _sportsRepositoryMock = Substitute.For<ISportsRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
        _handler = new DeleteSportCommandHandler(_sportsRepositoryMock, _unitOfWorkMock);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteSport_WhenSportExists()
    {
        // Arrange
        var sport = Sport.Create("Football", "Football description");
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns(sport);
        
        // Act
        await _handler.Handle(_command, default);
        
        // Assert
        _sportsRepositoryMock.Received(1).RemoveSport(sport);
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
    
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenSportDoesNotExist()
    {
        // Arrange
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns((Sport?)null);
        
        // Act
        var act = () => _handler.Handle(_command, default);
        
        // Assert
        await _unitOfWorkMock.DidNotReceive().CommitChangesAsync();
        _sportsRepositoryMock.DidNotReceive().RemoveSport(Arg.Any<Sport>());
        await act.Should().ThrowAsync<NotFoundException>();
    }
}