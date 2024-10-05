using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public class GetUserSportsQueryHandler(IUserSportsRepository userSportsRepository, IMapper mapper)
    : IRequestHandler<GetUserSportsQuery, ErrorOr<List<SportResponse>>>
{
    public async Task<ErrorOr<List<SportResponse>>> Handle(GetUserSportsQuery query, CancellationToken cancellationToken)
    {
        var sports = await userSportsRepository.GetUserSportsAsync(query.UserId);

        return mapper.Map<List<SportResponse>>(sports);
    }
}