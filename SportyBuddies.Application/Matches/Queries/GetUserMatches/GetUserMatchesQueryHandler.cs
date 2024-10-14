using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public class GetUserMatchesQueryHandler(
    IMatchesRepository matchesRepository,
    IUsersRepository usersRepository,
    IMapper mapper)
    : IRequestHandler<GetUserMatchesQuery, ErrorOr<List<MatchResponse>>>
{
    public async Task<ErrorOr<List<MatchResponse>>> Handle(GetUserMatchesQuery request,
        CancellationToken cancellationToken)
    {
        var matches = await matchesRepository.GetUserMatchesAsync(request.UserId);

        var matchResponses = new List<MatchResponse>();
        foreach (var match in matches)
        {
            var user = await usersRepository.GetUserByIdAsync(match.UserId);
            var matchedUser = await usersRepository.GetUserByIdAsync(match.MatchedUserId);

            var matchResponse = new MatchResponse(
                match.Id.Value,
                mapper.Map<UserResponse>(user),
                mapper.Map<UserResponse>(matchedUser),
                match.MatchDateTime,
                match.Swipe,
                match.SwipeDateTime
            );

            matchResponses.Add(matchResponse);
        }

        return matchResponses;
    }
}