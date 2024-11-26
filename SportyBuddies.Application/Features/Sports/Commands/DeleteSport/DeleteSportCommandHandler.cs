﻿using MediatR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Features.Sports.Commands.DeleteSport;

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