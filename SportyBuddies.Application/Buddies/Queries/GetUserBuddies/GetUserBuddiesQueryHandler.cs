﻿using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Buddies.Queries.GetUserBuddies;

public class GetUserBuddiesQueryHandler(IBuddiesRepository buddiesRepository, IMapper mapper)
    : IRequestHandler<GetUserBuddiesQuery, ErrorOr<object>>
{
    public async Task<ErrorOr<object>> Handle(GetUserBuddiesQuery query,
        CancellationToken cancellationToken)
    {
        var buddies = await buddiesRepository.GetUserBuddiesAsync(query.UserId, query.IncludeUsers);

        return query.IncludeUsers
            ? mapper.Map<List<BuddyWithUsersResponse>>(buddies)
            : mapper.Map<List<BuddyResponse>>(buddies);
    }
}