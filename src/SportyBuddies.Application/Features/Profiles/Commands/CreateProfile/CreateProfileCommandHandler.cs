using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.Features.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler(IProfilesRepository profilesRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProfileCommand, ProfileWithSportsResponse>
{
    public async Task<ProfileWithSportsResponse> Handle(CreateProfileCommand command,
        CancellationToken cancellationToken)
    {
        var profile = mapper.Map<Profile>(command);

        await profilesRepository.AddProfileAsync(profile);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<ProfileWithSportsResponse>(profile);
    }
}