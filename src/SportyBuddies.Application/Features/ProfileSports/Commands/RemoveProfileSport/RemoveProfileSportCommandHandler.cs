using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.ProfileSports.Commands.RemoveProfileSport;

public class RemoveProfileSportCommandHandler(
    IProfilesRepository iProfilesRepository,
    ISportsRepository sportsRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IRequestHandler<RemoveProfileSportCommand>
{
    public async Task Handle(RemoveProfileSportCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await iProfilesRepository.GetProfileByIdWithSportsAsync(currentUser.Id);
        if (user is null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);
        if (sport is null)
            throw new NotFoundException(nameof(sport), command.SportId.ToString());

        user.RemoveSport(sport);

        await unitOfWork.CommitChangesAsync();
    }
}