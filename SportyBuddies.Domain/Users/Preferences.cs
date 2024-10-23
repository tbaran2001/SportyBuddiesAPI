using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users;

public class Preferences : ValueObject
{
    public Preferences(int minAge, int maxAge, Gender gender)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        Gender = gender;
    }

    public Preferences()
    {
    }

    public int MinAge { get; }
    public int MaxAge { get; }
    public Gender Gender { get; }

    public static Preferences Create(int minAge, int maxAge, Gender gender)
    {
        return new Preferences(minAge, maxAge, gender);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return MinAge;
        yield return MaxAge;
        yield return Gender;
    }
}