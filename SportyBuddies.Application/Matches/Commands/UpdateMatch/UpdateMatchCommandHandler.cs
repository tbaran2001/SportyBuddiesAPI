using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandHandler(
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork,
    IMatchingService matchingService)
    : IRequestHandler<UpdateMatchCommand>
{
    public async Task Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetMatchByIdAsync(command.MatchId);
        if (match == null)
            throw new NotFoundException(nameof(match), command.MatchId.ToString());

        if(await matchingService.AreUsersBuddiesAsync(match.UserId, match.MatchedUserId))
            throw new BadRequestException("Users are already buddies");
        
        match.UpdateSwipe(command.Swipe);
        
        if (command.Swipe == Swipe.Right)
            await matchingService.CreateBuddyRelationshipAsync(match.Id);

        await unitOfWork.CommitChangesAsync();
    }
}