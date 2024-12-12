using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Application.Features.Profiles.Commands.UploadPhoto;

public class UploadPhotoCommandHandler(
    IProfilesRepository iProfilesRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IProfilePhotoService iProfilePhotoService)
    : IRequestHandler<UploadPhotoCommand, string>
{
    public async Task<string> Handle(UploadPhotoCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await iProfilesRepository.GetProfileByIdAsync(currentUser.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var photoUrl = await iProfilePhotoService.UploadAndAssignPhotoAsync(user, command.File, command.FileName, true);

        await unitOfWork.CommitChangesAsync();

        return photoUrl;
    }
}