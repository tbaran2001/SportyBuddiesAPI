using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public class GetRandomMatchQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    : IRequestHandler<GetRandomMatchQuery, RandomMatchResponse?>
{
    public async Task<RandomMatchResponse?> Handle(GetRandomMatchQuery query,
        CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetRandomMatchAsync(query.UserId);

        return mapper.Map<RandomMatchResponse>(match);
    }
}