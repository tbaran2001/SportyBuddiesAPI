using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public class GetRandomMatchQueryHandler : IRequestHandler<GetRandomMatchQuery, Match?>
{
    private readonly IMatchesRepository _matchesRepository;

    public GetRandomMatchQueryHandler(IMatchesRepository matchesRepository)
    {
        _matchesRepository = matchesRepository;
    }

    public async Task<Match?> Handle(GetRandomMatchQuery query, CancellationToken cancellationToken)
    {
        return await _matchesRepository.GetRandomMatchAsync(query.UserId);
    }
}