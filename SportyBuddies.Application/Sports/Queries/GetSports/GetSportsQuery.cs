using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public record GetSportsQuery : IRequest<IEnumerable<SportDto>>;