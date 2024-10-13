using ErrorOr;
using SportyBuddies.Domain.Common.Models;
using SportyBuddies.Domain.SportAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.Events;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    private readonly List<SportId> _sportIds = new();

    private User(UserId userId, string name, string description, DateTime lastActive) :
        base(userId)
    {
        Name = name;
        Description = description;
        LastActive = lastActive;
    }

    private User()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime LastActive { get; private set; } = DateTime.Now;
    public IReadOnlyList<SportId> SportIds => _sportIds.AsReadOnly();

    public static User Create(string name, string description, DateTime lastActive)
    {
        var user = new User(UserId.CreateUnique(), name, description, lastActive);

        return user;
    }

    public static User CreateWithId(Guid id, string name, string description, DateTime lastActive)
    {
        var user = new User(UserId.Create(id), name, description, lastActive);

        return user;
    }

    public void Delete()
    {
        AddDomainEvent(new UserDeletedEvent(Id));
    }

    public ErrorOr<Success> AddSport(SportId sportId)
    {
        if (SportIds.Contains(sportId))
            return Error.Conflict(description: "User already has this sport");

        _sportIds.Add(sportId);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveSport(SportId sportId)
    {
        if (!SportIds.Contains(sportId))
            return Error.NotFound(description: "User does not have this sport");

        _sportIds.Remove(sportId);
        return Result.Success;
    }

    public void RemoveAllSports()
    {
        _sportIds.Clear();
    }
}