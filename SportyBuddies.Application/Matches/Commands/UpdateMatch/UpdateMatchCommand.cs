using ErrorOr;
using MediatR;
using SportyBuddies.Domain.MatchAggregate;
using SportyBuddies.Domain.MatchAggregate.ValueObjects;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public record UpdateMatchCommand(MatchId MatchId, Swipe Swipe, DateTime SwipeDateTime) : IRequest<ErrorOr<Updated>>
{
    public UpdateMatchCommand(MatchId MatchId, Swipe Swipe) : this(MatchId, Swipe, DateTime.Now)
    {
    }
}