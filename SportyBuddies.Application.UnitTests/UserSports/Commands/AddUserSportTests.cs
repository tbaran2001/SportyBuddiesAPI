using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.UserSports.Commands.AddUserSport;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.UserSports.Commands;

public class AddUserSportTests
{
    private readonly AddUserSportCommand _command= new(Guid.NewGuid(), Guid.NewGuid());
    private readonly AddUserSportCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly ISportsRepository _sportsRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public AddUserSportTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _sportsRepositoryMock = Substitute.For<ISportsRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new AddUserSportCommandHandler(_usersRepositoryMock, _sportsRepositoryMock, _unitOfWorkMock);
    }
    
    [Fact]
    public async Task Handle_Should_AddSportToUser()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create("Football", "Description");
        _usersRepositoryMock.GetUserByIdWithSportsAsync(_command.UserId).Returns(user);
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns(sport);
        
        // Act
        await _handler.Handle(_command, default);
        
        // Assert
        user.Sports.Should().Contain(sport);
        
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenUserNotFound()
    {
        // Arrange
        _usersRepositoryMock.GetUserByIdWithSportsAsync(_command.UserId).Returns((User?)null);
        
        // Act
        var act = async () => await _handler.Handle(_command, default);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenSportNotFound()
    {
        // Arrange
        _usersRepositoryMock.GetUserByIdWithSportsAsync(_command.UserId).Returns(User.Create(Guid.NewGuid()));
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns((Sport?)null);
        
        // Act
        var act = async () => await _handler.Handle(_command, default);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}