using Profile.Domain.Common;

namespace Profile.Domain.ValueObjects;

public class ProfileName : ValueObject
{
    public string Value { get; }
    private ProfileName(string value) => Value = value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private ProfileName()
    {
    }
}