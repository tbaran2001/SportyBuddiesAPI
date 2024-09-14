using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandHandler : IRequestHandler<UpdateMatchCommand, ErrorOr<Updated>>
{
    private readonly IMapper _mapper;
    private readonly IMatchesRepository _matchesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMatchCommandHandler(IMatchesRepository matchesRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _matchesRepository = matchesRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        var match = await _matchesRepository.GetByIdAsync(command.MatchId);
        if (match == null) return Error.NotFound();

        _mapper.Map(command, match);

        await _unitOfWork.CommitChangesAsync();

        return Result.Updated;
    }
}