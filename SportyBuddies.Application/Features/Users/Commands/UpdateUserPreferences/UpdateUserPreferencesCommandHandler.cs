using MediatR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Users.Commands.UpdateUserPreferences;

public class UpdateUserPreferencesCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserPreferencesCommand>
{
    public async Task Handle(UpdateUserPreferencesCommand command,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(command.UserId);

        if (user == null)
            throw new NotFoundException(nameof(user), command.UserId.ToString());

        var preferences = Preferences.Create(command.MinAge, command.MaxAge,command.MaxDistance, command.Gender);

        user.UpdatePreferences(preferences);

        await unitOfWork.CommitChangesAsync();
    }
}