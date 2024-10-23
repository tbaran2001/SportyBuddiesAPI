using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Users.Queries.GetUserPhotos;

public class GetUserPhotosQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUserPhotosQuery, List<UserPhotoResponse>>
{
    public async Task<List<UserPhotoResponse>> Handle(GetUserPhotosQuery query,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithPhotosAsync(query.UserId);
        if (user == null)
            throw new NotFoundException(nameof(user), query.UserId.ToString());

        var photos = user.Photos;

        var photosResponse = photos.Select(p => new UserPhotoResponse(p.Url)).ToList();

        return photosResponse;
    }
}