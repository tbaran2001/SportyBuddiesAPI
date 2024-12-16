using ProfileManagement.Domain.Common;

namespace ProfileManagement.Domain.ValueObjects;

public class ProfileDescription : ValueObject
{
    public string Value { get; }
    private ProfileDescription(string value) => Value = value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private ProfileDescription()
    {
    }
}