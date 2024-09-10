using MediatR;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Queries.GetSport;

public record GetSportQuery(Guid SportId) : IRequest<Sport>;