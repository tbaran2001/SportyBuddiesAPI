using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.Services;

public class MatchingService(
    IUsersRepository usersRepository,
    IMatchesRepository matchesRepository,
    IBuddiesRepository buddiesRepository,
    IUnitOfWork unitOfWork)
    : IMatchingService
{
    public async Task FindMatchesAsync(Guid userId)
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

    public async Task CreateBuddyRelationshipAsync(Guid matchId)
    {
        var userMatch = await matchesRepository.GetMatchWithUsersByIdAsync(matchId);
        if (userMatch == null)
            return;

        var matchedUserMatch =
            await matchesRepository.GetMatchByUserAndMatchedUserAsync(userMatch.MatchedUser.Id, userMatch.User.Id);
        if (matchedUserMatch == null)
            return;

        if (userMatch.Swipe == Swipe.Right && matchedUserMatch?.Swipe == Swipe.Right)
        {
            var now = DateTime.Now;
            var userBuddy = Buddy.Create(userMatch.User, userMatch.MatchedUser, now);
            var matchedUserBuddy = Buddy.Create(userMatch.MatchedUser, userMatch.User, now);

            await buddiesRepository.AddBuddyAsync(userBuddy);
            await buddiesRepository.AddBuddyAsync(matchedUserBuddy);

            await unitOfWork.CommitChangesAsync();
        }
    }

    public async Task<bool> AreUsersBuddiesAsync(Guid userId, Guid matchedUserId)
    {
        return await buddiesRepository.AreUsersBuddiesAsync(userId, matchedUserId);
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
        return user.Sports.Intersect(matchedUser.Sports).Any();
    }

    private void AddNewMatches(User user, User matchedUser, IEnumerable<Match> existingMatches, List<Match> newMatches,
        DateTime now)
    {
        if (existingMatches.Any(m =>
                (m.User.Id == user.Id && m.MatchedUser.Id == matchedUser.Id) ||
                (m.User.Id == matchedUser.Id && m.MatchedUser.Id == user.Id)))
            return;

        newMatches.Add(Match.Create(user, matchedUser, now));
        newMatches.Add(Match.Create(matchedUser, user, now));
    }

    private void RemoveExistingMatches(User user, User matchedUser, IEnumerable<Match> existingMatches,
        List<Match> matchesToRemove)
    {
        var toRemove = existingMatches.Where(m =>
                (m.User.Id == user.Id && m.MatchedUser.Id == matchedUser.Id) ||
                (m.User.Id == matchedUser.Id && m.MatchedUser.Id == user.Id))
            .ToList();

        if (toRemove.Count != 0)
            matchesToRemove.AddRange(toRemove);
    }
}