using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Domain.Services;

public class MatchService(
    IUsersRepository usersRepository,
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork)
    : IMatchService
{
    public async Task FindMatchesToAddAsync(Guid userId)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        if (user.Sports.Count == 0)
            return;

        var potentialMatches = await usersRepository.GetPotentialMatchesAsync(userId, user.Sports.Select(s => s.Id));

        var newMatches = new List<Match>();

        foreach (var matchedUser in potentialMatches)
        {
            var (match1, match2) = Match.CreatePair(userId, matchedUser.Id, DateTime.UtcNow);
            newMatches.Add(match1);
            newMatches.Add(match2);
        }

        await matchesRepository.AddMatchesAsync(newMatches);
        await unitOfWork.CommitChangesAsync();
    }

    public async Task FindMatchesToRemoveAsync(Guid userId)
    {
        await matchesRepository.RemoveInvalidMatchesForUserAsync(userId);
    }
}