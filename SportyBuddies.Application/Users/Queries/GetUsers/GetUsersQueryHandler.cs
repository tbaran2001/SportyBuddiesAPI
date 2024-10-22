using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUsersQuery, ErrorOr<object>>
{
    public async Task<ErrorOr<object>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await usersRepository.GetAllUsersAsync(query.IncludeSports);

        return query.IncludeSports
            ? mapper.Map<List<UserWithSportsResponse>>(users)
            : mapper.Map<List<UserResponse>>(users);
    }
}