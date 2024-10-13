using SportyBuddies.Domain.Common.Models;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Domain.SportAggregate;

public class Sport : AggregateRoot<SportId>
{
    private Sport(SportId sportId, string name, string description) : base(sportId)
    {
        Name = name;
        Description = description;
    }

    private Sport()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public static Sport Create(string name, string description)
    {
        var sport = new Sport(SportId.CreateUnique(), name, description);

        return sport;
    }
}