﻿using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public class GetUserMatchesQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    : IRequestHandler<GetUserMatchesQuery, object>
{
    public async Task<object> Handle(GetUserMatchesQuery query,
        CancellationToken cancellationToken)
    {
        var matches =
            await matchesRepository.GetUserMatchesAsync(query.UserId);

        return mapper.Map<List<MatchResponse>>(matches);
    }
}