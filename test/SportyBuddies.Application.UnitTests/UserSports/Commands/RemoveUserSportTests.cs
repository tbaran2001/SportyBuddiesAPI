using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.UserSports.Commands.RemoveUserSport;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.UserSports.Commands;

public class RemoveUserSportTests
{
    private readonly RemoveUserSportCommand _command = new(Guid.NewGuid());
    private readonly RemoveUserSportCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly ISportsRepository _sportsRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;

    public RemoveUserSportTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _sportsRepositoryMock = Substitute.For<ISportsRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new RemoveUserSportCommandHandler(_usersRepositoryMock, _sportsRepositoryMock, _unitOfWorkMock,
            _userContextMock);
    }

    [Fact]
    public async Task Handle_Should_RemoveSportFromUser()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        var sport = Sport.Create("Football", "Description");
        user.AddSport(sport);
        _usersRepositoryMock.GetUserByIdWithSportsAsync(currentUser.Id).Returns(user);
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns(sport);

        // Act
        await _handler.Handle(_command, default);

        // Assert
        user.Sports.Should().NotContain(sport);

        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdWithSportsAsync(currentUser.Id).Returns((User?)null);

        // Act
        var act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenSportNotFound()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdWithSportsAsync(currentUser.Id).Returns(user);
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns((Sport?)null);

        // Act
        var act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}