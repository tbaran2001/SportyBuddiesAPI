using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Profiles.Commands.DeleteProfile;

public class DeleteProfileCommandHandler(IProfilesRepository profilesRepository, IUnitOfWork unitOfWork,IUserContext userContext)
    : IRequestHandler<DeleteProfileCommand>
{
    public async Task Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdAsync(currentUser.Id);
        if (profile == null)
            throw new NotFoundException(nameof(profile), currentUser.Id.ToString());

        profile.Delete();

        profilesRepository.RemoveProfile(profile);
        await unitOfWork.CommitChangesAsync();
    }
}