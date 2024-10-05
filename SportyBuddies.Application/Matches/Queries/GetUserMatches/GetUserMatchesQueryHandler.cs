using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public class GetUserMatchesQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    : IRequestHandler<GetUserMatchesQuery, ErrorOr<List<MatchResponse>>>
{
    public async Task<ErrorOr<List<MatchResponse>>> Handle(GetUserMatchesQuery request, CancellationToken cancellationToken)
    {
        var matches = await matchesRepository.GetUserMatchesAsync(request.UserId);
        return mapper.Map<List<MatchResponse>>(matches);
    }
}