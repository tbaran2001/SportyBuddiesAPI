﻿using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Sports.Queries.GetSport;

public class GetSportQueryHandler : IRequestHandler<GetSportQuery, SportResponse>
{
    private readonly IMapper _mapper;
    private readonly ISportsRepository _sportsRepository;

    public GetSportQueryHandler(ISportsRepository sportsRepository, IMapper mapper)
    {
        _sportsRepository = sportsRepository;
        _mapper = mapper;
    }

    public async Task<SportResponse> Handle(GetSportQuery query, CancellationToken cancellationToken)
    {
        var sport = await _sportsRepository.GetSportByIdAsync(query.SportId);

        if (sport == null)
            throw new NotFoundException(nameof(sport), query.SportId.ToString());

        return _mapper.Map<SportResponse>(sport);
    }
}