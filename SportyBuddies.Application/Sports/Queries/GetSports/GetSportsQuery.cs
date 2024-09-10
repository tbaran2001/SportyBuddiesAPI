using MediatR;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public record GetSportsQuery : IRequest<IEnumerable<Sport>>;