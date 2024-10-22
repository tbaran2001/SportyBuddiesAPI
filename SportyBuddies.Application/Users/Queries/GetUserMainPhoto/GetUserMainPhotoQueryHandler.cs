using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUserMainPhoto;

public class GetUserMainPhotoQueryHandler(IUsersRepository usersRepository)
    : IRequestHandler<GetUserMainPhotoQuery, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(GetUserMainPhotoQuery request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithPhotosAsync(request.UserId);
        if (user == null)
            return Error.NotFound();

        if (user.MainPhoto == null)
            return Error.NotFound();

        var mainPhoto = user.MainPhoto;

        return mainPhoto.Url;
    }
}