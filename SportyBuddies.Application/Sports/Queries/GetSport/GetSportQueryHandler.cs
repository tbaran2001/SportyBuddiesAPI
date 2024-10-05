using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Queries.GetSport;

public class GetSportQueryHandler : IRequestHandler<GetSportQuery, ErrorOr<SportDto>>
{
    private readonly IMapper _mapper;
    private readonly ISportsRepository _sportsRepository;

    public GetSportQueryHandler(ISportsRepository sportsRepository, IMapper mapper)
    {
        _sportsRepository = sportsRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<SportDto>> Handle(GetSportQuery query, CancellationToken cancellationToken)
    {
        var sport = await _sportsRepository.GetSportByIdAsync(query.SportId);

        if (sport == null) 
            return Error.NotFound();

        return _mapper.Map<SportDto>(sport);
    }
}