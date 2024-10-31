using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UploadPhoto;

public class UploadPhotoCommandHandler(
    IFileStorageService fileStorageService,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadPhotoCommand, string>
{
    public async Task<string> Handle(UploadPhotoCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithPhotosAsync(command.UserId);
        if (user == null)
            throw new NotFoundException(nameof(user), command.UserId.ToString());

        var photoId = Guid.NewGuid();
        var url = await fileStorageService.SaveFileAsync(user.Id, command.File, photoId);

        var userPhoto = UserPhoto.Create(user, url, command.IsMain);

        user.AddPhoto(userPhoto);

        await unitOfWork.CommitChangesAsync();

        return url;
    }
}