using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;

public class GetUserMainPhotoQueryHandler(IUsersRepository usersRepository, IUserContext userContext)
    : IRequestHandler<GetUserMainPhotoQuery, string>
{
    public async Task<string> Handle(GetUserMainPhotoQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdWithPhotosAsync(currentUser!.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        if (user.MainPhoto == null)
            throw new NotFoundException(nameof(user.MainPhoto), currentUser.Id.ToString());

        var mainPhoto = user.MainPhoto;

        return mainPhoto.Url;
    }
}