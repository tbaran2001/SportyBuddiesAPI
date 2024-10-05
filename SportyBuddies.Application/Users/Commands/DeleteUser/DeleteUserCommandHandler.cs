using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUsersRepository sportsRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await sportsRepository.GetUserByIdAsync(command.UserId);

        if (user == null) 
            return Error.NotFound();

        user.Delete();

        sportsRepository.RemoveUser(user);
        await unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}