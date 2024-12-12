using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Matches.Queries.GetProfileMatches;

public class GetProfileMatchesQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    : IRequestHandler<GetProfileMatchesQuery, List<MatchResponse>>
{
    public async Task<List<MatchResponse>> Handle(GetProfileMatchesQuery query,
        CancellationToken cancellationToken)
    {
        var matches =
            await matchesRepository.GetProfileMatchesAsync(query.ProfileId);

        return mapper.Map<List<MatchResponse>>(matches);
    }
}