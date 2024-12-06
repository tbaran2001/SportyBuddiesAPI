using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Users.Commands.UpdateUserPreferences;

public class UpdateUserPreferencesCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext
) : IRequestHandler<UpdateUserPreferencesCommand>
{
    public async Task Handle(UpdateUserPreferencesCommand command,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdAsync(currentUser.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var preferences = Preferences.Create(command.MinAge, command.MaxAge, command.MaxDistance, command.Gender);

        user.UpdatePreferences(preferences);

        await unitOfWork.CommitChangesAsync();
    }
}