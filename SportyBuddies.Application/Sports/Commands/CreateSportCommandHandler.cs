using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Commands;

public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, ErrorOr<Sport>>
{
    private readonly ISportsRepository _sportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork)
    {
        _sportsRepository = sportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Sport>> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var sport = new Sport(request.Name, request.Description);

        await _sportsRepository.AddSportAsync(sport);
        await _unitOfWork.CommitChangesAsync();

        return sport;
    }
}