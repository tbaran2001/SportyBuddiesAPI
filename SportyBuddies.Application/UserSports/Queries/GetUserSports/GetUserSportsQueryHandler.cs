using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public class GetUserSportsQueryHandler : IRequestHandler<GetUserSportsQuery, IEnumerable<Sport>>
{
    private readonly IUserSportsRepository _userSportsRepository;

    public GetUserSportsQueryHandler(IUserSportsRepository userSportsRepository)
    {
        _userSportsRepository = userSportsRepository;
    }

    public async Task<IEnumerable<Sport>> Handle(GetUserSportsQuery query, CancellationToken cancellationToken)
    {
        return await _userSportsRepository.GetUserSportsAsync(query.UserId);
    }
}