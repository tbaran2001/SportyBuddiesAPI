using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Profiles.Commands.UpdateProfilePreferences;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.UnitTests.Profiles.Commands;

public class UpdateProfilePreferencesTests
{
    private readonly UpdateProfilePreferencesCommand _command = new(18, 21, 5, Gender.Male);
    private readonly UpdateProfilePreferencesCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;

    public UpdateProfilePreferencesTests()
    {
        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new UpdateProfilePreferencesCommandHandler(_profilesRepositoryMock, _unitOfWorkMock, _userContextMock);
    }

    [Fact]
    public async Task Handle_ShouldUpdateUserPreferences_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdAsync(currentUser.Id).Returns(user);

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

        _profilesRepositoryMock.GetProfileByIdAsync(currentUser.Id).Returns((Profile?)null);

        // Act
        var act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}