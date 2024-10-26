using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Application.Buddies.Queries.GetUserBuddies;

public class GetUserBuddiesQueryHandler(IBuddiesRepository buddiesRepository, IMapper mapper)
    : IRequestHandler<GetUserBuddiesQuery, object>
{
    public async Task<object> Handle(GetUserBuddiesQuery query,
        CancellationToken cancellationToken)
    {
        var buddies = await buddiesRepository.GetUserBuddiesAsync(query.UserId, query.IncludeUsers);

        return query.IncludeUsers
            ? mapper.Map<List<BuddyWithUsersResponse>>(buddies)
            : mapper.Map<List<BuddyResponse>>(buddies);
    }
}