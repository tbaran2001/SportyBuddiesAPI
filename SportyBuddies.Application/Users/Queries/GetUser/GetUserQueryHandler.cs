using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public class GetUserQueryHandler: IRequestHandler<GetUserQuery, User>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }


    public async Task<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(query.UserId);
        
        if (user == null) throw new NotFoundException(nameof(user), query.UserId.ToString());

        return user;
    }
}