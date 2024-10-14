using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public class GetRandomMatchQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    : IRequestHandler<GetRandomMatchQuery, ErrorOr<MatchResponse?>>
{
    public async Task<ErrorOr<MatchResponse?>> Handle(GetRandomMatchQuery query, CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetRandomMatchAsync(query.UserId);
        return mapper.Map<MatchResponse>(match);
    }
}