﻿using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
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
        var validator = new CreateSportCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false) throw new ValidationException(validationResult.ToDictionary());

        var sport = _mapper.Map<Sport>(request);

        await _sportsRepository.AddSportAsync(sport);
        await _unitOfWork.CommitChangesAsync();

        return sport;
    }
}