using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Domain.Users;

public class User
{
    public User(string name, string description, DateTime lastActive, ICollection<Sport> sports, Guid? id = null)
    {
        Name = name;
        Description = description;
        LastActive = lastActive;
        Sports = sports;
        Id = id ?? Guid.NewGuid();
    }

    public User()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime LastActive { get; private set; }
    public ICollection<Sport> Sports { get; private set; }
}