using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Buddies.Queries.GetProfileBuddies;

public class GetProfileBuddiesQueryHandler(IBuddiesRepository buddiesRepository, IMapper mapper,IUserContext userContext)
    : IRequestHandler<GetProfileBuddiesQuery, List<BuddyResponse>>
{
    public async Task<List<BuddyResponse>> Handle(GetProfileBuddiesQuery query,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var buddies = await buddiesRepository.GetProfileBuddiesAsync(currentUser.Id);

        return mapper.Map<List<BuddyResponse>>(buddies);
    }
}