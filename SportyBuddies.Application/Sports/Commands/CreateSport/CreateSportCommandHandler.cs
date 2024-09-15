using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, ErrorOr<SportDto>>
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

    public async Task<ErrorOr<SportDto>> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var sport = _mapper.Map<Sport>(request);

        await _sportsRepository.AddAsync(sport);
        await _unitOfWork.CommitChangesAsync();

        return _mapper.Map<SportDto>(sport);
    }
}