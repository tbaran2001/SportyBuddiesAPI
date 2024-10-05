using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public class GetSportsQueryHandler(ISportsRepository sportsRepository, IMapper mapper)
    : IRequestHandler<GetSportsQuery, ErrorOr<List<SportDto>>>
{
    public async Task<ErrorOr<List<SportDto>>> Handle(GetSportsQuery query, CancellationToken cancellationToken)
    {
        var sports = await sportsRepository.GetAllAsync();

        return mapper.Map<List<SportDto>>(sports);
    }
}