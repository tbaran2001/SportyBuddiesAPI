using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public class GetRandomMatchQueryHandler(
    IMatchesRepository matchesRepository,
    IUsersRepository usersRepository,
    IMapper mapper)
    : IRequestHandler<GetRandomMatchQuery, ErrorOr<MatchResponse?>>
{
    public async Task<ErrorOr<MatchResponse?>> Handle(GetRandomMatchQuery query, CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetRandomMatchAsync(query.UserId);

        if (match == null)
            return Error.NotFound();

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

        return matchResponse;
    }
}