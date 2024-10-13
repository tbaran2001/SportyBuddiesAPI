using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.MatchAggregate;
using SportyBuddies.Domain.UserAggregate;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Common.Services;

public class MatchingService(
    IUsersRepository usersRepository,
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork)
    : IMatchingService
{
    public async Task FindMatchesAsync(UserId userId)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(userId);

        if (user == null)
            return;

        var allUsers = (await usersRepository.GetAllUsersWithSportsAsync()).ToList();
        var existingMatches = (await matchesRepository.GetUserExistingMatchesAsync(userId)).ToList();

        var newMatches = new List<Match>();
        var matchesToRemove = new List<Match>();

        ProcessMatches(user, allUsers, existingMatches, newMatches, matchesToRemove);

        await matchesRepository.AddMatchesAsync(newMatches);
        matchesRepository.RemoveMatches(matchesToRemove);

        await unitOfWork.CommitChangesAsync();
    }

    private void ProcessMatches(User user, List<User> allUsers, List<Match> existingMatches, List<Match> newMatches,
        List<Match> matchesToRemove)
    {
        foreach (var matchedUser in allUsers)
        {
            if (user.Id == matchedUser.Id)
                continue;

            var now = DateTime.Now;

            if (HasCommonSports(user, matchedUser))
                AddNewMatches(user, matchedUser, existingMatches, newMatches, now);
            else
                RemoveExistingMatches(user, matchedUser, existingMatches, matchesToRemove);
        }
    }

    private bool HasCommonSports(User user, User matchedUser)
    {
        return user.SportIds.Intersect(matchedUser.SportIds).Any();
    }

    private void AddNewMatches(User user, User matchedUser, IEnumerable<Match> existingMatches, List<Match> newMatches,
        DateTime now)
    {
        if (existingMatches.Any(m =>
                (m.UserId == user.Id && m.MatchedUserId == matchedUser.Id) ||
                (m.UserId == matchedUser.Id && m.MatchedUserId == user.Id)))
            return;

        newMatches.Add(Match.Create(user.Id, matchedUser.Id, now, null, null));
        newMatches.Add(Match.Create(matchedUser.Id, user.Id, now, null, null));
    }

    private void RemoveExistingMatches(User user, User matchedUser, IEnumerable<Match> existingMatches,
        List<Match> matchesToRemove)
    {
        var toRemove = existingMatches.Where(m =>
                (m.UserId == user.Id && m.MatchedUserId == matchedUser.Id) ||
                (m.UserId == matchedUser.Id && m.MatchedUserId == user.Id))
            .ToList();

        if (toRemove.Count != 0)
            matchesToRemove.AddRange(toRemove);
    }
}