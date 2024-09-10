using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public class GetUserMatchesQueryHandler : IRequestHandler<GetUserMatchesQuery, IEnumerable<MatchDto>>
{
    private readonly IMapper _mapper;
    private readonly IMatchesRepository _matchesRepository;

    public GetUserMatchesQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    {
        _matchesRepository = matchesRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MatchDto>> Handle(GetUserMatchesQuery request, CancellationToken cancellationToken)
    {
        var matches = await _matchesRepository.GetUserMatchesAsync(request.UserId);
        return _mapper.Map<IEnumerable<MatchDto>>(matches);
    }
}