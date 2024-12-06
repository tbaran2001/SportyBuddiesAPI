using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Queries;

public class GetUserMainPhotoTests
{
    private readonly GetUserMainPhotoQuery _query = new();
    private readonly GetUserMainPhotoQueryHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUserContext _userContextMock;

    public GetUserMainPhotoTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new GetUserMainPhotoQueryHandler(_usersRepositoryMock, _userContextMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnMainPhotoUrl_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", [], null);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        var mainPhoto = UserPhoto.Create(user, "url", true);
        user.AddPhoto(mainPhoto);

        _usersRepositoryMock.GetUserByIdWithPhotosAsync(currentUser.Id).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().Be("url");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", [], null);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdWithPhotosAsync(currentUser.Id).Returns((User?)null);

        // Act
        var act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotHaveMainPhoto()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", [], null);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdWithPhotosAsync(currentUser.Id).Returns(user);

        // Act
        var act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}