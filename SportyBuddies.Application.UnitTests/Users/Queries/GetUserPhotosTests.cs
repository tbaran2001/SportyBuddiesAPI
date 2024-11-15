using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Queries.GetUserPhotos;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Queries;

public class GetUserPhotosTests
{
    private readonly GetUserPhotosQuery _query = new(Guid.NewGuid());
    private readonly GetUserPhotosQueryHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;

    public GetUserPhotosTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _handler = new GetUserPhotosQueryHandler(_usersRepositoryMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnPhotos_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(_query.UserId);
        var photo1 = UserPhoto.Create(user, "url1", false);
        var photo2 = UserPhoto.Create(user, "url2", false);
        user.AddPhoto(photo1);
        user.AddPhoto(photo2);
        _usersRepositoryMock.GetUserByIdWithPhotosAsync(_query.UserId).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainSingle(p => p.Url == "url1");
        result.Should().ContainSingle(p => p.Url == "url2");
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
    public async Task Handle_ShouldReturnEmptyList_WhenUserDoesNotHavePhotos()
    {
        // Arrange
        var user = User.Create(_query.UserId);
        _usersRepositoryMock.GetUserByIdWithPhotosAsync(_query.UserId).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeEmpty();
    }
}