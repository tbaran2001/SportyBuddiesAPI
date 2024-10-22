using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UploadPhoto;

public class UploadPhotoCommandHandler(
    IFileStorageService fileStorageService,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadPhotoCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(UploadPhotoCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithPhotosAsync(command.UserId);
        if (user == null)
            return Error.NotFound();

        var photoId = Guid.NewGuid();
        var url = await fileStorageService.SaveFileAsync(user.Id, command.File, photoId);

        var userPhoto = new UserPhoto(user, url, command.IsMain, photoId);

        user.AddPhoto(userPhoto);

        await unitOfWork.CommitChangesAsync();

        return url;
    }
}