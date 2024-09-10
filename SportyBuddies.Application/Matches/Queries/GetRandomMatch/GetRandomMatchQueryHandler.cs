using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public class GetRandomMatchQueryHandler : IRequestHandler<GetRandomMatchQuery, MatchDto?>
{
    private readonly IMapper _mapper;
    private readonly IMatchesRepository _matchesRepository;

    public GetRandomMatchQueryHandler(IMatchesRepository matchesRepository, IMapper mapper)
    {
        _matchesRepository = matchesRepository;
        _mapper = mapper;
    }

    public async Task<MatchDto?> Handle(GetRandomMatchQuery query, CancellationToken cancellationToken)
    {
        var match = await _matchesRepository.GetRandomMatchAsync(query.UserId);
        return _mapper.Map<MatchDto>(match);
    }
}