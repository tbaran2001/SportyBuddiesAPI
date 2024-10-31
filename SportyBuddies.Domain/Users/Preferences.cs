using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users;

public class Preferences : ValueObject
{
    private Preferences(int minAge, int maxAge, Gender gender)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        Gender = gender;
    }

    public int MinAge { get; private set; }
    public int MaxAge { get; private set; }
    public Gender Gender { get; private set; }

    public static Preferences Create(int minAge, int maxAge, Gender gender)
    {
        if (minAge < 0 || maxAge < 0)
        {
            throw new ArgumentException("Age cannot be negative");
        }

        if (minAge > maxAge)
        {
            throw new ArgumentException("Min age cannot be greater than max age");
        }

        return new Preferences(minAge, maxAge, gender);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return MinAge;
        yield return MaxAge;
        yield return Gender;
    }

    private Preferences()
    {
    }
}