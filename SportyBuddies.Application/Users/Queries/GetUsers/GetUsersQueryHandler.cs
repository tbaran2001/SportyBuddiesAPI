using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUsersQuery, ErrorOr<List<UserWithSportsResponse>>>
{
    public async Task<ErrorOr<List<UserWithSportsResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await usersRepository.GetAllUsersAsync();

        return mapper.Map<List<UserWithSportsResponse>>(users);
    }
}