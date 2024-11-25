using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Services;

namespace SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandHandler(
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork,
    IMatchService matchingService,
    IBuddyService buddyService)
    : IRequestHandler<UpdateMatchCommand>
{
    public async Task Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetMatchByIdAsync(command.MatchId);
        if (match == null)
            throw new NotFoundException(nameof(match), command.MatchId.ToString());

        var oppositeMatch = await matchesRepository.GetMatchByIdAsync(match.OppositeMatchId);
        if(oppositeMatch == null)
            throw new NotFoundException(nameof(oppositeMatch), command.MatchId.ToString());

        match.UpdateSwipe(command.Swipe);
        
        await buddyService.AddBuddy(match, oppositeMatch);
        await unitOfWork.CommitChangesAsync();
    }
}