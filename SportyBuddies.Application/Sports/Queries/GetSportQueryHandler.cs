using MediatR;
using ErrorOr;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Queries;

public class GetSportQueryHandler : IRequestHandler<GetSportQuery, ErrorOr<Sport>>
{
    private readonly ISportsRepository _sportsRepository;

    public GetSportQueryHandler(ISportsRepository sportsRepository)
    {
        _sportsRepository = sportsRepository;
    }

    public async Task<ErrorOr<Sport>> Handle(GetSportQuery query, CancellationToken cancellationToken)
    {
        var sport = await _sportsRepository.GetByIdAsync(query.SportId);

        return sport is null
            ? Error.NotFound(description: "Sport not found")
            : sport;
    }
}