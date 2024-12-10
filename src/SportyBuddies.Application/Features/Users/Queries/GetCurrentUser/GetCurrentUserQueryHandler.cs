using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Application.Features.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler(
    IUsersRepository usersRepository,
    IMapper mapper,
    IUserContext userContext,
    IBlobStorageService blobStorageService)
    : IRequestHandler<GetCurrentUserQuery, UserWithSportsResponse>
{
    public async Task<UserWithSportsResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdWithSportsAsync(currentUser.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var mainPhotoSasUrl = blobStorageService.GetBlobSasUrl(user.MainPhotoUrl);

        var userWithSportsResponse = mapper.Map<UserWithSportsResponse>(user);
        userWithSportsResponse.MainPhotoSasUrl = mainPhotoSasUrl;

        return userWithSportsResponse;
    }
}