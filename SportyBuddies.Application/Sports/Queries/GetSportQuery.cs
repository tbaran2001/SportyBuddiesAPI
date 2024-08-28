using MediatR;
using ErrorOr;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Queries;

public record GetSportQuery(Guid SportId) : IRequest<ErrorOr<Sport>>;