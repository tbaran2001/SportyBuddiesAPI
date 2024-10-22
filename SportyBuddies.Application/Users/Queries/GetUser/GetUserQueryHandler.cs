using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    : IRequestHandler<GetUserQuery, ErrorOr<UserWithSportsResponse>>
{
    public async Task<ErrorOr<UserWithSportsResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(query.UserId);

        if (user == null)
            return Error.NotFound();

        return mapper.Map<UserWithSportsResponse>(user);
    }
}