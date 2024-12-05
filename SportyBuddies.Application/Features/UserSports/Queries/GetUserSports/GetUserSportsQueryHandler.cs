using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.UserSports.Queries.GetUserSports;

public class GetUserSportsQueryHandler(IUsersRepository usersRepository, IMapper mapper,IUserContext userContext)
    : IRequestHandler<GetUserSportsQuery, List<SportResponse>>
{
    public async Task<List<SportResponse>> Handle(GetUserSportsQuery query,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdWithSportsAsync(currentUser.Id);
        if (user is null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var sports = user.Sports;

        return mapper.Map<List<SportResponse>>(sports);
    }
}