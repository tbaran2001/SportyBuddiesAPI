using MediatR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUsersRepository sportsRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await sportsRepository.GetUserByIdAsync(command.UserId);

        if (user == null)
            throw new NotFoundException(nameof(user), command.UserId.ToString());

        user.Delete();

        sportsRepository.RemoveUser(user);
        await unitOfWork.CommitChangesAsync();
    }
}