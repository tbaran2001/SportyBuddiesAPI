using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.Common.Interfaces.Repositories;

public interface IProfilesRepository
{
    Task<Profile?> GetProfileByIdAsync(Guid profileId);
    Task<Profile?> GetProfileByIdWithSportsAsync(Guid profileId);
    Task<IEnumerable<Profile>> GetAllProfilesAsync();
    Task AddProfileAsync(Profile profile);
    void RemoveProfile(Profile profile);
    Task<IEnumerable<Profile>> GetPotentialMatchesAsync(Guid profileId,IEnumerable<Guid> profileSports);
}