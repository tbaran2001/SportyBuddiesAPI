using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(
    IProfilesRepository iProfilesRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IRequestHandler<UpdateProfileCommand, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(UpdateProfileCommand command,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await iProfilesRepository.GetProfileByIdAsync(currentUser.Id);
        if (profile == null)
            throw new NotFoundException(nameof(profile), currentUser.Id.ToString());

        profile.Update(command.Name, command.Description, command.DateOfBirth, command.Gender);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<ProfileResponse>(profile);
    }
}