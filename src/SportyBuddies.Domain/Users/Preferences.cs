using System.Text.Json.Serialization;
using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users;

public class Preferences : ValueObject
{
    public static Preferences Default => new Preferences(18, 45, 50, 0);
    [JsonConstructor]
    private Preferences(int minAge, int maxAge, int maxDistance, Gender gender)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        MaxDistance = maxDistance;
        Gender = gender;
    }

    public int MinAge { get; private set; }
    public int MaxAge { get; private set; }
    public int MaxDistance { get; private set; }
    public Gender Gender { get; private set; }

    public static Preferences Create(int minAge, int maxAge, int maxDistance, Gender gender)
    {
        if (minAge < 0 || maxAge < 0)
        {
            throw new ArgumentException("Age cannot be negative");
        }

        if (minAge > maxAge)
        {
            throw new ArgumentException("Min age cannot be greater than max age");
        }

        if (maxDistance is < 1 or > 100)
        {
            throw new ArgumentException("Max distance must be in range from 1 to 100");
        }

        return new Preferences(minAge, maxAge, maxDistance, gender);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return MinAge;
        yield return MaxAge;
        yield return MaxDistance;
        yield return Gender;
    }

    private Preferences()
    {
    }
}