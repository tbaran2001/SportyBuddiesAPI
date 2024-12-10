using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Application.Features.Users.Queries.GetUser;

public class GetUserQueryHandler(
    IUsersRepository usersRepository,
    IMapper mapper,
    IBlobStorageService blobStorageService)
    : IRequestHandler<GetUserQuery, UserWithSportsResponse>
{
    public async Task<UserWithSportsResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(query.UserId);
        if (user == null)
            throw new NotFoundException(nameof(user), query.UserId.ToString());

        var mainPhotoSasUrl = blobStorageService.GetBlobSasUrl(user.MainPhotoUrl);

        var userWithSportsResponse = mapper.Map<UserWithSportsResponse>(user);
        userWithSportsResponse.MainPhotoSasUrl = mainPhotoSasUrl;

        return userWithSportsResponse;
    }
}