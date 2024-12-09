using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Commands.UpdateUserPreferences;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Commands;

public class UpdateUserPreferencesTests
{
    private readonly UpdateUserPreferencesCommand _command = new(18, 21, 5, Gender.Male);
    private readonly UpdateUserPreferencesCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;

    public UpdateUserPreferencesTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new UpdateUserPreferencesCommandHandler(_usersRepositoryMock, _unitOfWorkMock, _userContextMock);
    }

    [Fact]
    public async Task Handle_ShouldUpdateUserPreferences_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdAsync(currentUser.Id).Returns(user);

        // Act
        await _handler.Handle(_command, default);

        // Assert
        user.Preferences.MinAge.Should().Be(_command.MinAge);
        user.Preferences.MaxAge.Should().Be(_command.MaxAge);
        user.Preferences.MaxDistance.Should().Be(_command.MaxDistance);
        user.Preferences.Gender.Should().Be(_command.Gender);

        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdAsync(currentUser.Id).Returns((User?)null);

        // Act
        var act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}