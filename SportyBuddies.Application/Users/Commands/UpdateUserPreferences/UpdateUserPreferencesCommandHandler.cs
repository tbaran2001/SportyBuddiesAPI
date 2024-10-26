using MediatR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUserPreferences;

public class UpdateUserPreferencesCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserPreferencesCommand>
{
    public async Task Handle(UpdateUserPreferencesCommand request,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(request.UserId);

        if (user == null)
            throw new NotFoundException(nameof(user), request.UserId.ToString());

        var preferences = Preferences.Create(request.MinAge, request.MaxAge, request.Gender);

        user.UpdatePreferences(preferences);

        await unitOfWork.CommitChangesAsync();
    }
}