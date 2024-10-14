using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public class GetUserSportsQueryHandler(
    IUsersRepository usersRepository,
    ISportsRepository sportsRepository,
    IMapper mapper)
    : IRequestHandler<GetUserSportsQuery, ErrorOr<List<SportResponse>>>
{
    public async Task<ErrorOr<List<SportResponse>>> Handle(GetUserSportsQuery query,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(query.UserId);
        if (user is null)
            return Error.NotFound();

        var sports = await sportsRepository.GetSportsByIdsAsync(user.SportIds);

        return mapper.Map<List<SportResponse>>(sports);
    }
}