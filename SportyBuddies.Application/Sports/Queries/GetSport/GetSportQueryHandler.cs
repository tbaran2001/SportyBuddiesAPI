using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Queries.GetSport;

public class GetSportQueryHandler : IRequestHandler<GetSportQuery, Sport>
{
    private readonly ISportsRepository _sportsRepository;

    public GetSportQueryHandler(ISportsRepository sportsRepository)
    {
        _sportsRepository = sportsRepository;
    }

    public async Task<Sport> Handle(GetSportQuery query, CancellationToken cancellationToken)
    {
        var sport = await _sportsRepository.GetByIdAsync(query.SportId);

        if (sport == null) throw new NotFoundException(nameof(sport), query.SportId.ToString());

        return sport;
    }
}