using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;

public class GetRandomMatchQueryHandler(IMatchesRepository matchesRepository, IMapper mapper, IUserContext userContext)
    : IRequestHandler<GetRandomMatchQuery, RandomMatchResponse?>
{
    public async Task<RandomMatchResponse?> Handle(GetRandomMatchQuery query,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var match = await matchesRepository.GetRandomMatchAsync(currentUser!.Id);

        return mapper.Map<RandomMatchResponse>(match);
    }
}