using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUsersRepository usersRepository, ISportsRepository sportsRepository, IMapper mapper)
    : IRequestHandler<GetUserQuery, ErrorOr<UserWithSportsResponse>>
{
    public async Task<ErrorOr<UserWithSportsResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(query.UserId);

        if (user == null)
            return Error.NotFound();

        var sports = await sportsRepository.GetSportsByIdsAsync(user.SportIds);

        var userWithSports = new UserWithSportsResponse(user.Id.Value, user.Name, user.Description, user.LastActive,
            mapper.Map<List<SportResponse>>(sports));

        return userWithSports;
    }
}