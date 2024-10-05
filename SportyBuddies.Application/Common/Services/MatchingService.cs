using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.Services;

public class MatchingService(
    IUserSportsRepository userSportsRepository,
    IUsersRepository usersRepository,
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork)
    : IMatchingService
{
    private readonly IUserSportsRepository _userSportsRepository = userSportsRepository;

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

        newMatches.Add(new Match(user, matchedUser, now, null, null));
        newMatches.Add(new Match(matchedUser, user, now, null, null));
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