using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Features.Matches.Queries.GetUserMatches;

public class GetUserMatchesQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    : IRequestHandler<GetUserMatchesQuery, List<MatchResponse>>
{
    public async Task<List<MatchResponse>> Handle(GetUserMatchesQuery query,
        CancellationToken cancellationToken)
    {
        var matches =
            await matchesRepository.GetUserMatchesAsync(query.UserId);

        return mapper.Map<List<MatchResponse>>(matches);
    }
}