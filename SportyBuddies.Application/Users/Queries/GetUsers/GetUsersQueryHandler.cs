using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUsersQuery, object>
{
    public async Task<object> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await usersRepository.GetAllUsersAsync(query.IncludeSports);

        return query.IncludeSports
            ? mapper.Map<List<UserWithSportsResponse>>(users)
            : mapper.Map<List<UserResponse>>(users);
    }
}