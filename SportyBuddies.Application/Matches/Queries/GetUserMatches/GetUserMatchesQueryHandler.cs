using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public class GetUserMatchesQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    : IRequestHandler<GetUserMatchesQuery, ErrorOr<object>>
{
    public async Task<ErrorOr<object>> Handle(GetUserMatchesQuery query,
        CancellationToken cancellationToken)
    {
        var matches =
            await matchesRepository.GetUserMatchesAsync(query.UserId, query.IncludeUsers);

        return query.IncludeUsers
            ? mapper.Map<List<MatchWithUsersResponse>>(matches)
            : mapper.Map<List<MatchResponse>>(matches);
    }
}