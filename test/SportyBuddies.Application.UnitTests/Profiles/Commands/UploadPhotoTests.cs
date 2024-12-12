using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Profiles.Commands.UploadPhoto;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.UnitTests.Profiles.Commands;

public class UploadPhotoTests
{
    private readonly UploadPhotoCommand _command = new(new MemoryStream(),"url");
    private readonly UploadPhotoCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IFormFile _fileMock;
    private readonly IUserContext _userContextMock;
    private readonly IProfilePhotoService _iProfilePhotoServiceMock;

    public UploadPhotoTests()
    {
        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();
        _iProfilePhotoServiceMock = Substitute.For<IProfilePhotoService>();

        _handler = new UploadPhotoCommandHandler(_profilesRepositoryMock, _unitOfWorkMock,
            _userContextMock, _iProfilePhotoServiceMock);

        _fileMock = Substitute.For<IFormFile>();
    }

    [Fact]
    public async Task Handle_ShouldReturnUrl_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdAsync(currentUser.Id).Returns(user);
        _iProfilePhotoServiceMock.UploadAndAssignPhotoAsync(user, Arg.Any<Stream>(), _command.FileName, true)
            .Returns("url");

        // Act
        var result = await _handler.Handle(_command, default);

        // Assert
        result.Should().Be("url");
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