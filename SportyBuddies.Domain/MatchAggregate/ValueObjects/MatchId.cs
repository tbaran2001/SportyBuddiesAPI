using SportyBuddies.Domain.Common.Models;

namespace SportyBuddies.Domain.MatchAggregate.ValueObjects;

public sealed class MatchId : ValueObject
{
    private MatchId(Guid value)
    {
        Value = value;
    }

    private MatchId()
    {
    }

    public Guid Value { get; }

    public static MatchId CreateUnique()
    {
        return new MatchId(Guid.NewGuid());
    }

    public static MatchId Create(Guid value)
    {
        return new MatchId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}