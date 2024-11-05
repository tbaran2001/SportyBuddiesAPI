﻿using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public record GetSportsQuery : IRequest<List<SportResponse>>;