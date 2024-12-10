using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Users.Commands.UploadPhoto;

public class UploadPhotoCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IUserPhotoService userPhotoService)
    : IRequestHandler<UploadPhotoCommand, string>
{
    public async Task<string> Handle(UploadPhotoCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdWithPhotosAsync(currentUser.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var photoUrl = await userPhotoService.UploadAndAssignPhotoAsync(user, command.File, command.FileName, true);

        await unitOfWork.CommitChangesAsync();

        return photoUrl;
    }
}