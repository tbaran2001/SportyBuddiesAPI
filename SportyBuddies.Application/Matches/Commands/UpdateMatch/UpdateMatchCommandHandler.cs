using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandHandler(IMatchesRepository matchesRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateMatchCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetByIdAsync(command.MatchId);
        if (match == null) 
            return Error.NotFound();

        mapper.Map(command, match);

        await unitOfWork.CommitChangesAsync();

        return Result.Updated;
    }
}