using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.SportAggregate;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public class CreateSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateSportCommand, ErrorOr<SportResponse>>
{
    public async Task<ErrorOr<SportResponse>> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var sport = Sport.Create(name: request.Name, description: request.Description);

        await sportsRepository.AddSportAsync(sport);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<SportResponse>(sport);
    }
}