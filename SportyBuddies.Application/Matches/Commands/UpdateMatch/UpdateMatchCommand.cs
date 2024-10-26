using MediatR;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public record UpdateMatchCommand(Guid MatchId, Swipe Swipe) : IRequest;