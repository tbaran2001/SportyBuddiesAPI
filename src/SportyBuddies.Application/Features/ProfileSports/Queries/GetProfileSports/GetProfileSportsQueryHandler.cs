using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.ProfileSports.Queries.GetProfileSports;

public class GetProfileSportsQueryHandler(IProfilesRepository iProfilesRepository, IMapper mapper,IUserContext userContext)
    : IRequestHandler<GetProfileSportsQuery, List<SportResponse>>
{
    public async Task<List<SportResponse>> Handle(GetProfileSportsQuery query,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await iProfilesRepository.GetProfileByIdWithSportsAsync(currentUser.Id);
        if (user is null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var sports = user.Sports;

        return mapper.Map<List<SportResponse>>(sports);
    }
}