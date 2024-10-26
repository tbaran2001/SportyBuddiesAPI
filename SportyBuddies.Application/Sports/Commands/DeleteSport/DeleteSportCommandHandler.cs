using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Sports.Commands.DeleteSport;

public class DeleteSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSportCommand>
{
    public async Task Handle(DeleteSportCommand command, CancellationToken cancellationToken)
    {
        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);

        if (sport == null)
            throw new NotFoundException(nameof(sport), command.SportId.ToString());

        sportsRepository.RemoveSport(sport);
        await unitOfWork.CommitChangesAsync();
    }
}