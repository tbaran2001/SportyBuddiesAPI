using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Commands.UploadPhoto;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Commands;

public class UploadPhotoTests
{
    private readonly UploadPhotoCommand _command = new(new MemoryStream(),"url");
    private readonly UploadPhotoCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IFormFile _fileMock;
    private readonly IUserContext _userContextMock;
    private readonly IUserPhotoService _userPhotoServiceMock;

    public UploadPhotoTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();
        _userPhotoServiceMock = Substitute.For<IUserPhotoService>();

        _handler = new UploadPhotoCommandHandler(_usersRepositoryMock, _unitOfWorkMock,
            _userContextMock, _userPhotoServiceMock);

        _fileMock = Substitute.For<IFormFile>();
    }

    [Fact]
    public async Task Handle_ShouldReturnUrl_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdAsync(currentUser.Id).Returns(user);
        _userPhotoServiceMock.UploadAndAssignPhotoAsync(user, Arg.Any<Stream>(), _command.FileName, true)
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

        _usersRepositoryMock.GetUserByIdAsync(currentUser.Id).Returns((User?)null);

        // Act
        var act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}