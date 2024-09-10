using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime LastActive { get; private set; } = DateTime.Now;
    public ICollection<Sport> Sports { get; private set; }

    public User(string name, string description, DateTime lastActive, ICollection<Sport> sports, Guid? id = null)
    {
        Name = name;
        Description = description;
        Sports = sports;
        LastActive = lastActive;
        Id = id ?? Guid.NewGuid();
    }

    public User()
    {
    }
}