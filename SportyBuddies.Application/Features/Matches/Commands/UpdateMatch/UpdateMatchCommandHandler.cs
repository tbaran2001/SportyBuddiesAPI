using MediatR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandHandler(
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork,
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
        
        await buddyService.AddBuddyAsync(match, oppositeMatch);
        await unitOfWork.CommitChangesAsync();
    }
}