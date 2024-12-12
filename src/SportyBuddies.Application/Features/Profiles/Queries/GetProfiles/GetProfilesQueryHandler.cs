using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Profiles.Queries.GetProfiles;

public class GetProfilesQueryHandler(IProfilesRepository iProfilesRepository, IMapper mapper)
    : IRequestHandler<GetProfilesQuery, List<ProfileWithSportsResponse>>
{
    public async Task<List<ProfileWithSportsResponse>> Handle(GetProfilesQuery query, CancellationToken cancellationToken)
    {
        var profiles = await iProfilesRepository.GetAllProfilesAsync();

        return mapper.Map<List<ProfileWithSportsResponse>>(profiles);
    }
}