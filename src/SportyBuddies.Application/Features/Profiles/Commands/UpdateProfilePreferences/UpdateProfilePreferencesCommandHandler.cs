using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.Features.Profiles.Commands.UpdateProfilePreferences;

public class UpdateProfilePreferencesCommandHandler(
    IProfilesRepository profilesRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext
) : IRequestHandler<UpdateProfilePreferencesCommand>
{
    public async Task Handle(UpdateProfilePreferencesCommand command,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdAsync(currentUser.Id);
        if (profile == null)
            throw new NotFoundException(nameof(profile), currentUser.Id.ToString());

        var preferences = Preferences.Create(command.MinAge, command.MaxAge, command.MaxDistance, command.Gender);

        profile.UpdatePreferences(preferences);

        await unitOfWork.CommitChangesAsync();
    }
}