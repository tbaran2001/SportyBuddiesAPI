using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, Sport>
{
    private readonly ISportsRepository _sportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork)
    {
        _sportsRepository = sportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Sport> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var sport = new Sport(request.Name, request.Description, new List<User>());

        await _sportsRepository.AddSportAsync(sport);
        await _unitOfWork.CommitChangesAsync();

        return sport;
    }
}