using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public class GetUserSportsQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUserSportsQuery, ErrorOr<List<SportResponse>>>
{
    public async Task<ErrorOr<List<SportResponse>>> Handle(GetUserSportsQuery query,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(query.UserId);
        if (user is null)
            return Error.NotFound();

        var sports = user.Sports;

        return mapper.Map<List<SportResponse>>(sports);
    }
}