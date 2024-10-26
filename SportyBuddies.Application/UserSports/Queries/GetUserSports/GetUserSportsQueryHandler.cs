using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public class GetUserSportsQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUserSportsQuery, List<SportResponse>>
{
    public async Task<List<SportResponse>> Handle(GetUserSportsQuery query,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(query.UserId);
        if (user is null)
            throw new NotFoundException(nameof(user), query.UserId.ToString());

        var sports = user.Sports;

        return mapper.Map<List<SportResponse>>(sports);
    }
}