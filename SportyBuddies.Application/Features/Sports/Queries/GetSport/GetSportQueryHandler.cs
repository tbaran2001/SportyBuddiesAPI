using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Features.Sports.Queries.GetSport;

public class GetSportQueryHandler(ISportsRepository sportsRepository, IMapper mapper)
    : IRequestHandler<GetSportQuery, SportResponse>
{
    public async Task<SportResponse> Handle(GetSportQuery query, CancellationToken cancellationToken)
    {
        var sport = await sportsRepository.GetSportByIdAsync(query.SportId);

        if (sport == null)
            throw new NotFoundException(nameof(sport), query.SportId.ToString());

        return mapper.Map<SportResponse>(sport);
    }
}