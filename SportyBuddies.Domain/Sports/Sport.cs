using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Sports;

public class Sport : Entity
{
    public Sport(string name, string description, ICollection<User> users, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Users = users;
    }

    public Sport()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<User> Users { get; private set; }
}