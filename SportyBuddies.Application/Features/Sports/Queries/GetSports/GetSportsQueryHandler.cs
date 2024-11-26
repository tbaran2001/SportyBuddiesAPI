﻿using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Features.Sports.Queries.GetSports;

public class GetSportsQueryHandler(ISportsRepository sportsRepository, IMapper mapper)
    : IRequestHandler<GetSportsQuery, List<SportResponse>>
{
    public async Task<List<SportResponse>> Handle(GetSportsQuery query, CancellationToken cancellationToken)
    {
        var sports = await sportsRepository.GetAllSportsAsync();

        return mapper.Map<List<SportResponse>>(sports);
    }
}