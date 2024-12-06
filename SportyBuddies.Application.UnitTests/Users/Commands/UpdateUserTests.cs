using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Commands.UpdateUser;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Commands;

public class UpdateUserTests
{
    private readonly UpdateUserCommand _command = new("username", "description", Gender.Male,
        new DateOnly(2000, 1, 1));

    private readonly UpdateUserCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;

    public UpdateUserTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<UserMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new UpdateUserCommandHandler(_usersRepositoryMock, mapper, _unitOfWorkMock, _userContextMock);
    }

    [Fact]
    public async Task Handle_ShouldUpdateUser_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", [], null);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdAsync(currentUser.Id).Returns(user);
        // Act
        var result = await _handler.Handle(_command, default);

        // Assert
        user.Name.Should().Be(_command.Name);
        user.Description.Should().Be(_command.Description);
        user.Gender.Should().Be(_command.Gender);
        user.DateOfBirth.Should().Be(_command.DateOfBirth);

        await _unitOfWorkMock.Received(1).CommitChangesAsync();

        result.Should().BeOfType<UserResponse>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", [], null);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdAsync(currentUser.Id).Returns((User?)null);

        // Act
        var act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}