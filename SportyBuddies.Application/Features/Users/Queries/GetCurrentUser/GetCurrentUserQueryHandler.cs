using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler(IUsersRepository usersRepository, IMapper mapper, IUserContext userContext)
    : IRequestHandler<GetCurrentUserQuery, UserWithSportsResponse>
{
    public async Task<UserWithSportsResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdWithSportsAsync(currentUser!.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        return mapper.Map<UserWithSportsResponse>(user);
    }
}