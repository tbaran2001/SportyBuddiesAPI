using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Sports;

public class Sport : Entity
{
    private Sport(
        Guid id,
        string name,
        string description
    ) : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public static Sport Create(string name, string description)
    {
        return new Sport(
            id: Guid.NewGuid(),
            name: name,
            description: description);
    }

    private Sport()
    {
    }
}