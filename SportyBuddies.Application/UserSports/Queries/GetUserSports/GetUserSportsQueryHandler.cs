using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public class GetUserSportsQueryHandler : IRequestHandler<GetUserSportsQuery, IEnumerable<SportDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserSportsRepository _userSportsRepository;

    public GetUserSportsQueryHandler(IUserSportsRepository userSportsRepository, IMapper mapper)
    {
        _userSportsRepository = userSportsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SportDto>> Handle(GetUserSportsQuery query, CancellationToken cancellationToken)
    {
        var sports = await _userSportsRepository.GetUserSportsAsync(query.UserId);

        return _mapper.Map<IEnumerable<SportDto>>(sports);
    }
}