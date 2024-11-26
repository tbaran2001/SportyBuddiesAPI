using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Queries;

public class GetUserMainPhotoTests
{
    private readonly GetUserMainPhotoQuery _query= new(Guid.NewGuid());
    private readonly GetUserMainPhotoQueryHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;

    public GetUserMainPhotoTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _handler = new GetUserMainPhotoQueryHandler(_usersRepositoryMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnMainPhotoUrl_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(_query.UserId);
        var mainPhoto = UserPhoto.Create(user, "url", true);
        user.AddPhoto(mainPhoto);
        _usersRepositoryMock.GetUserByIdWithPhotosAsync(_query.UserId).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().Be("url");
    }
    
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        _usersRepositoryMock.GetUserByIdWithPhotosAsync(_query.UserId).Returns((User?)null);

        // Act
        var act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotHaveMainPhoto()
    {
        // Arrange
        var user = User.Create(_query.UserId);
        _usersRepositoryMock.GetUserByIdWithPhotosAsync(_query.UserId).Returns(user);

        // Act
        var act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}