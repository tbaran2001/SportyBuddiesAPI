using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Commands.DeleteSport;

public class DeleteSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSportCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteSportCommand command, CancellationToken cancellationToken)
    {
        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);

        if (sport == null)
            return Error.NotFound();

        sportsRepository.RemoveSport(sport);
        await unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}