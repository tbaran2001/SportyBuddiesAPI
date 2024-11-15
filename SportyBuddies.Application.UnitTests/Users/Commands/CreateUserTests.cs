using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Commands.CreateUser;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Commands;

public class CreateUserTests
{
    private readonly CreateUserCommand _command = new("username", "description");
    private readonly CreateUserCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;


    public CreateUserTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMappingProfile>();
            cfg.AddProfile<SportMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateUserCommandHandler(_usersRepositoryMock, mapper, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenValidRequest()
    {
        // Act
        var result = await _handler.Handle(_command, default);

        // Assert
        result.Should().BeOfType<UserWithSportsResponse>();
        result.Name.Should().Be(_command.Name);
        result.Description.Should().Be(_command.Description);

        await _usersRepositoryMock.Received(1).AddUserAsync(Arg.Any<User>());
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
}