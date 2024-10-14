﻿using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public class CreateSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateSportCommand, ErrorOr<SportResponse>>
{
    public async Task<ErrorOr<SportResponse>> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var sport = mapper.Map<Sport>(request);

        await sportsRepository.AddSportAsync(sport);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<SportResponse>(sport);
    }
}