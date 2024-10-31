using NSubstitute;
using SportyBuddies.Application.Users.Commands.DeleteUser;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Commands;

public class DeleteUserTests
{
    private readonly DeleteUserCommand _command = new(Guid.NewGuid());
    private readonly DeleteUserCommandHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    
    public DeleteUserTests()
    {
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new DeleteUserCommandHandler(_usersRepositoryMock, _unitOfWorkMock);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteUser_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(_command.UserId);
        _usersRepositoryMock.GetUserByIdAsync(_command.UserId).Returns(user);
        
        // Act
        await _handler.Handle(_command, default);
        
        // Assert
        _usersRepositoryMock.Received(1).RemoveUser(user);
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
}