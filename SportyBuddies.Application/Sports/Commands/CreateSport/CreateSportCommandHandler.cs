using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, Sport>
{
    private readonly IMapper _mapper;
    private readonly ISportsRepository _sportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _sportsRepository = sportsRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Sport> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var sport = _mapper.Map<Sport>(request);

        await _sportsRepository.AddSportAsync(sport);
        await _unitOfWork.CommitChangesAsync();

        return sport;
    }
}