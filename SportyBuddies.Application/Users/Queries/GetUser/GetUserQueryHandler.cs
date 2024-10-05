using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUserQuery, ErrorOr<UserDto>>
{
    public async Task<ErrorOr<UserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(query.UserId);

        if (user == null) 
            return Error.NotFound();

        return mapper.Map<UserDto>(user);
    }
}