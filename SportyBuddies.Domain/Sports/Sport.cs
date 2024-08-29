namespace SportyBuddies.Domain.Sports;

public class Sport
{
    public Sport(string name, string description, Guid? id = null)
    {
        Name = name;
        Description = description;
        Id = id ?? Guid.NewGuid();
    }

    public Sport()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
}