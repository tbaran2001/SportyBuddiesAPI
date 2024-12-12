using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Application.Features.Profiles.Queries.GetCurrentProfile;

public class GetCurrentProfileQueryHandler(
    IProfilesRepository profilesRepository,
    IMapper mapper,
    IUserContext userContext,
    IBlobStorageService blobStorageService)
    : IRequestHandler<GetCurrentProfileQuery, ProfileWithSportsResponse>
{
    public async Task<ProfileWithSportsResponse> Handle(GetCurrentProfileQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUser.Id);
        if (profile == null)
            throw new NotFoundException(nameof(profile), currentUser.Id.ToString());

        var mainPhotoSasUrl = blobStorageService.GetBlobSasUrl(profile.MainPhotoUrl);

        var profileWithSportsResponse = mapper.Map<ProfileWithSportsResponse>(profile);
        profileWithSportsResponse.MainPhotoSasUrl = mainPhotoSasUrl;

        return profileWithSportsResponse;
    }
}