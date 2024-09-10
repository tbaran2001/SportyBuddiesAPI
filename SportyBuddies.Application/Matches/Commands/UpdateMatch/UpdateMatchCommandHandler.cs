using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandHandler: IRequestHandler<UpdateMatchCommand>
{
    private readonly IMatchesRepository _matchesRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMatchCommandHandler(IMatchesRepository matchesRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _matchesRepository = matchesRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        var match = await _matchesRepository.GetByIdAsync(command.MatchId);
        if (match == null) throw new NotFoundException(nameof(Match), command.MatchId.ToString());
        
        _mapper.Map(command, match);
        
        await _unitOfWork.CommitChangesAsync();
    }
}