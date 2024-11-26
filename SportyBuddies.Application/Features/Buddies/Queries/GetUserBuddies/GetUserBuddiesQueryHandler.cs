using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Buddies.Queries.GetUserBuddies;

public class GetUserBuddiesQueryHandler(IBuddiesRepository buddiesRepository, IMapper mapper)
    : IRequestHandler<GetUserBuddiesQuery, List<BuddyResponse>>
{
    public async Task<List<BuddyResponse>> Handle(GetUserBuddiesQuery query,
        CancellationToken cancellationToken)
    {
        var buddies = await buddiesRepository.GetUserBuddiesAsync(query.UserId);

        return mapper.Map<List<BuddyResponse>>(buddies);
    }
}