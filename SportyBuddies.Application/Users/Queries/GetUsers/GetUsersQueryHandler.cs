using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly IUsersRepository _usersRepository;

    public GetUsersQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetAllAsync();

        return users;
    }
}