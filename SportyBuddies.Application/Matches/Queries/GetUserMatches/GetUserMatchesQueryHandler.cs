using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public class GetUserMatchesQueryHandler : IRequestHandler<GetUserMatchesQuery, IEnumerable<Match>>
{
    private readonly IMatchesRepository _matchesRepository;

    public GetUserMatchesQueryHandler(IMatchesRepository matchesRepository)
    {
        _matchesRepository = matchesRepository;
    }

    public async Task<IEnumerable<Match>> Handle(GetUserMatchesQuery request, CancellationToken cancellationToken)
    {
        return await _matchesRepository.GetUserMatchesAsync(request.UserId);
    }
}