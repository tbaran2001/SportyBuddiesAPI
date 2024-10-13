using SportyBuddies.Domain.Common.Models;

namespace SportyBuddies.Domain.SportAggregate.ValueObjects;

public class SportId : ValueObject
{
    private SportId(Guid value)
    {
        Value = value;
    }

    private SportId()
    {
    }

    public Guid Value { get; }

    public static SportId CreateUnique()
    {
        return new SportId(Guid.NewGuid());
    }

    public static SportId Create(Guid value)
    {
        return new SportId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}