using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public class GetSportsQueryHandler : IRequestHandler<GetSportsQuery, ErrorOr<List<SportDto>>>
{
    private readonly IMapper _mapper;
    private readonly ISportsRepository _sportsRepository;

    public GetSportsQueryHandler(ISportsRepository sportsRepository, IMapper mapper)
    {
        _sportsRepository = sportsRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<SportDto>>> Handle(GetSportsQuery query, CancellationToken cancellationToken)
    {
        var sports = await _sportsRepository.GetAllAsync();

        return _mapper.Map<List<SportDto>>(sports);
    }
}