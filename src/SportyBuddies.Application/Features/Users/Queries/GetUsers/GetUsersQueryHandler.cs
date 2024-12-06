using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUsersQuery, List<UserWithSportsResponse>>
{
    public async Task<List<UserWithSportsResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await usersRepository.GetAllUsersAsync();

        return mapper.Map<List<UserWithSportsResponse>>(users);
    }
}