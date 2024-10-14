using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Buddies.Queries.GetUserBuddies;

public class GetUserBuddiesQueryHandler(IBuddiesRepository buddiesRepository, IMapper mapper)
    : IRequestHandler<GetUserBuddiesQuery, ErrorOr<List<BuddyResponse>>>
{
    public async Task<ErrorOr<List<BuddyResponse>>> Handle(GetUserBuddiesQuery query,
        CancellationToken cancellationToken)
    {
        var buddies = await buddiesRepository.GetUserBuddiesAsync(query.UserId);

        return mapper.Map<List<BuddyResponse>>(buddies);
    }
}