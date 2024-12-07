﻿using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Users.Commands.UploadPhoto;

public class UploadPhotoCommandHandler(
    IFileStorageService fileStorageService,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IRequestHandler<UploadPhotoCommand, string>
{
    public async Task<string> Handle(UploadPhotoCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdWithPhotosAsync(currentUser.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var photoId = Guid.NewGuid();
        var url = await fileStorageService.SaveFileAsync(user.Id, command.File, photoId);

        var userPhoto = UserPhoto.Create(user, url, command.IsMain);

        user.AddPhoto(userPhoto);

        await unitOfWork.CommitChangesAsync();

        return url;
    }
}