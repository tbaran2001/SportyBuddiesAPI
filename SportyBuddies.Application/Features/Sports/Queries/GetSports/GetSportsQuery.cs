using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Features.Sports.Queries.GetSports;

public record GetSportsQuery : IRequest<List<SportResponse>>;