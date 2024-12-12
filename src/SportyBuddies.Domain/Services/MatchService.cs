using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Domain.Services;

public class MatchService(
    IProfilesRepository iProfilesRepository,
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork)
    : IMatchService
{
    public async Task FindMatchesToAddAsync(Guid profileId)
    {
        var profile = await iProfilesRepository.GetProfileByIdWithSportsAsync(profileId);
        if (profile == null)
            throw new Exception("Profile not found");
        if (profile.Sports.Count == 0)
            return;

        var potentialMatches = await iProfilesRepository.GetPotentialMatchesAsync(profileId, profile.Sports.Select(s => s.Id));

        var newMatches = new List<Match>();

        foreach (var matchedProfile in potentialMatches)
        {
            var (match1, match2) = Match.CreatePair(profileId, matchedProfile.Id, DateTime.UtcNow);
            newMatches.Add(match1);
            newMatches.Add(match2);
        }

        await matchesRepository.AddMatchesAsync(newMatches);
        await unitOfWork.CommitChangesAsync();
    }

    public async Task FindMatchesToRemoveAsync(Guid profileId)
    {
        await matchesRepository.RemoveInvalidMatchesForProfileAsync(profileId);
    }
}