using Profile.Domain.Common;
using Profile.Domain.Enums;
using Profile.Domain.Exceptions;
using Profile.Domain.Models;

namespace Profile.Domain.ValueObjects;

public record Preferences
{
    public int MinAge { get; } = default!;
    public int MaxAge { get; } = default!;
    public int MaxDistance { get; } = default!;
    public Gender PreferredGender { get; } = default!;

    private Preferences(int minAge, int maxAge, int maxDistance, Gender preferredGender)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        MaxDistance = maxDistance;
        PreferredGender = preferredGender;
    }

    public static Preferences Create(int minAge, int maxAge, int maxDistance, Gender preferredGender)
    {
        if (minAge < 0 || maxAge < 0)
            throw new DomainException("Age cannot be negative");
        if (minAge > maxAge)
            throw new DomainException("Min age cannot be greater than max age");
        if (maxDistance is < 1 or > 100)
            throw new DomainException("Max distance must be in range from 1 to 100");

        return new Preferences(minAge, maxAge, maxDistance, preferredGender);
    }

    private Preferences()
    {
    }
}