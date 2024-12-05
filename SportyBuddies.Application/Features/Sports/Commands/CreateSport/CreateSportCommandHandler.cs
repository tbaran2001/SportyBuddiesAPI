using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Features.Sports.Commands.CreateSport;

public class CreateSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateSportCommand, SportResponse>
{
    public async Task<SportResponse> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var sport = mapper.Map<Sport>(request);
        
        if (await sportsRepository.SportNameExistsAsync(sport.Name))
        {
            throw new ConflictException($"Sport with name {sport.Name} already exists.");
        }

        await sportsRepository.AddSportAsync(sport);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<SportResponse>(sport);
    }
}