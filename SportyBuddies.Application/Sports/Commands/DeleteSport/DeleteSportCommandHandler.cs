using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Commands.DeleteSport;

public class DeleteSportCommandHandler : IRequestHandler<DeleteSportCommand, ErrorOr<Deleted>>
{
    private readonly ISportsRepository _sportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork)
    {
        _sportsRepository = sportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSportCommand command, CancellationToken cancellationToken)
    {
        var sport = await _sportsRepository.GetByIdAsync(command.SportId);

        if (sport == null) return Error.NotFound();

        _sportsRepository.Remove(sport);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}