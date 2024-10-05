using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users.Events;

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
    public ICollection<Sport> Sports { get; private set; }

    public void Delete()
    {
        DomainEvents.Add(new UserDeletedEvent(Id));
    }
}