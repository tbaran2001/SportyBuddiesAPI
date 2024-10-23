using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUserPreferences;

public class UpdateUserPreferencesCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserPreferencesCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateUserPreferencesCommand request,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(request.UserId);

        if (user == null)
            return Error.NotFound();

        var preferences = Preferences.Create(request.MinAge, request.MaxAge, request.Gender);

        user.UpdatePreferences(preferences);

        await unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}