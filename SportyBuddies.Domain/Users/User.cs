using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users.Events;
using ErrorOr;

namespace SportyBuddies.Domain.Users;

public class User : Entity
{
    public User(string name, string description, DateTime lastActive, ICollection<Sport> sports, Guid? id = null) :
        base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Sports = sports;
        LastActive = lastActive;
    }

    public User()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime LastActive { get; private set; } = DateTime.Now;
    public ICollection<Sport> Sports { get; private set; } = new List<Sport>();

    public void Delete()
    {
        DomainEvents.Add(new UserDeletedEvent(Id));
    }

    public ErrorOr<Success> AddSport(Sport sport)
    {
        if (Sports.Contains(sport))
            return Error.Conflict(description: "User already has this sport");

        Sports.Add(sport);
        return Result.Success;
    }
    
    public ErrorOr<Success> RemoveSport(Sport sport)
    {
        if (!Sports.Contains(sport))
            return Error.NotFound(description: "User does not have this sport");

        Sports.Remove(sport);
        return Result.Success;
    }
    
    public void RemoveAllSports()
    {
        Sports.Clear();
    }
}