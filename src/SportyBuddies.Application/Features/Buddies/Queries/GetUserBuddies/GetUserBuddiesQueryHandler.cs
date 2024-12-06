using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Buddies.Queries.GetUserBuddies;

public class GetUserBuddiesQueryHandler(IBuddiesRepository buddiesRepository, IMapper mapper,IUserContext userContext)
    : IRequestHandler<GetUserBuddiesQuery, List<BuddyResponse>>
{
    public async Task<List<BuddyResponse>> Handle(GetUserBuddiesQuery query,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var buddies = await buddiesRepository.GetUserBuddiesAsync(currentUser.Id);

        return mapper.Map<List<BuddyResponse>>(buddies);
    }
}