using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUserPhotos;

public class GetUserPhotosQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUserPhotosQuery, ErrorOr<List<UserPhotoResponse>>>
{
    public async Task<ErrorOr<List<UserPhotoResponse>>> Handle(GetUserPhotosQuery request,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithPhotosAsync(request.UserId);
        if (user == null)
            return Error.NotFound();

        var photos = user.Photos;

        var photosResponse = photos.Select(p => new UserPhotoResponse(p.Url)).ToList();

        return photosResponse;
    }
}