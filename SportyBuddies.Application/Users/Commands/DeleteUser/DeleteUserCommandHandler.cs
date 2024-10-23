using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

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