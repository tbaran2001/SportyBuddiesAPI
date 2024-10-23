using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUserQuery, UserWithSportsResponse>
{
    public async Task<UserWithSportsResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(query.UserId);

        if (user == null)
            throw new NotFoundException(nameof(user), query.UserId.ToString());

        return mapper.Map<UserWithSportsResponse>(user);
    }
}