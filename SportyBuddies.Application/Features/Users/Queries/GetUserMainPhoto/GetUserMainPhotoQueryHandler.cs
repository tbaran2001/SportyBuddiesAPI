﻿using MediatR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;

public class GetUserMainPhotoQueryHandler(IUsersRepository usersRepository)
    : IRequestHandler<GetUserMainPhotoQuery, string>
{
    public async Task<string> Handle(GetUserMainPhotoQuery request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithPhotosAsync(request.UserId);
        if (user == null)
            throw new NotFoundException(nameof(user), request.UserId.ToString());

        if (user.MainPhoto == null)
            throw new NotFoundException(nameof(user.MainPhoto), request.UserId.ToString());

        var mainPhoto = user.MainPhoto;

        return mainPhoto.Url;
    }
}