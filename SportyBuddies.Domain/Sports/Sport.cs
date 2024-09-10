using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Sports;

public class Sport
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<User> Users { get; private set; }
    
    public Sport(string name, string description, ICollection<User> users, Guid? id = null)
    {
        Name = name;
        Description = description;
        Users = users;
        Id = id ?? Guid.NewGuid();
    }

    public Sport()
    {
    }
}