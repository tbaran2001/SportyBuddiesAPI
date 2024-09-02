using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Commands.DeleteSport;

public class DeleteSportCommandHandler : IRequestHandler<DeleteSportCommand>
{
    private readonly ISportsRepository _sportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork)
    {
        _sportsRepository = sportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSportCommand command, CancellationToken cancellationToken)
    {
        var sport = await _sportsRepository.GetByIdAsync(command.SportId);


        await _sportsRepository.RemoveSportAsync(sport);
        await _unitOfWork.CommitChangesAsync();
    }
}