using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandHandler(
    IMatchesRepository matchesRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IMatchingService matchingService)
    : IRequestHandler<UpdateMatchCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetMatchByIdAsync(command.MatchId);
        if (match == null)
            return Error.NotFound();

        mapper.Map(command, match);

        if (command.Swipe == Swipe.Right)
            await matchingService.CreateBuddyRelationshipAsync(match.Id);

        await unitOfWork.CommitChangesAsync();

        return Result.Updated;
    }
}