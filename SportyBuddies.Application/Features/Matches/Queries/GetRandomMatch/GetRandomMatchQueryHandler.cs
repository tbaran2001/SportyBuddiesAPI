using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;

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