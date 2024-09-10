using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public class GetSportsQueryHandler : IRequestHandler<GetSportsQuery, IEnumerable<Sport>>
{
    private readonly ISportsRepository _sportsRepository;

    public GetSportsQueryHandler(ISportsRepository sportsRepository)
    {
        _sportsRepository = sportsRepository;
    }

    public async Task<IEnumerable<Sport>> Handle(GetSportsQuery query, CancellationToken cancellationToken)
    {
        var sports = await _sportsRepository.GetAllAsync();

        return sports;
    }
}