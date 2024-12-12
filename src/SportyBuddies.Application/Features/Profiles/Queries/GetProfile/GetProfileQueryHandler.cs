using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Application.Features.Profiles.Queries.GetProfile;

public class GetProfileQueryHandler(
    IProfilesRepository iProfilesRepository,
    IMapper mapper,
    IBlobStorageService blobStorageService)
    : IRequestHandler<GetProfileQuery, ProfileWithSportsResponse>
{
    public async Task<ProfileWithSportsResponse> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await iProfilesRepository.GetProfileByIdWithSportsAsync(query.ProfileId);
        if (profile == null)
            throw new NotFoundException(nameof(profile), query.ProfileId.ToString());

        var mainPhotoSasUrl = blobStorageService.GetBlobSasUrl(profile.MainPhotoUrl);

        var profileWithSportsResponse = mapper.Map<ProfileWithSportsResponse>(profile);
        profileWithSportsResponse.MainPhotoSasUrl = mainPhotoSasUrl;

        return profileWithSportsResponse;
    }
}