using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Features.Users.Commands.DeleteUser;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Commands;

public class DeleteUserTests
{
    private readonly DeleteUserCommand _command = new();
    private readonly DeleteUserCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;
    
    public DeleteUserTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new DeleteUserCommandHandler(_usersRepositoryMock, _unitOfWorkMock, _userContextMock);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteUser_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdAsync(currentUser.Id).Returns(user);
        
        // Act
        await _handler.Handle(_command, default);
        
        // Assert
        _usersRepositoryMock.Received(1).RemoveUser(user);
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
}