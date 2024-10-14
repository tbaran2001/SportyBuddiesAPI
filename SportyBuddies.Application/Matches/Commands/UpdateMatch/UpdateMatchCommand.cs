using ErrorOr;
using MediatR;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public record UpdateMatchCommand(Guid MatchId, Swipe Swipe, DateTime SwipeDateTime) : IRequest<ErrorOr<Updated>>
{
    public UpdateMatchCommand(Guid MatchId, Swipe Swipe) : this(MatchId, Swipe, DateTime.Now)
    {
    }
}