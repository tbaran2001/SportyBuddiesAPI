using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUsersRepository sportsRepository, IUnitOfWork unitOfWork,IUserContext userContext)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await sportsRepository.GetUserByIdAsync(currentUser!.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        user.Delete();

        sportsRepository.RemoveUser(user);
        await unitOfWork.CommitChangesAsync();
    }
}