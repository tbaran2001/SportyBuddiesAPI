using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestData;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class CreatePreferencesTests
{
    [Fact]
    public void Create_ShouldReturnPreferences_WhenValidValues()
    {
        // Act
        var preferences = Preferences.Create(
            PreferencesData.MinAge,
            PreferencesData.MaxAge,
            PreferencesData.MaxDistance,
            PreferencesData.Gender);

        // Assert
        preferences.MinAge.Should().Be(PreferencesData.MinAge);
        preferences.MaxAge.Should().Be(PreferencesData.MaxAge);
        preferences.MaxDistance.Should().Be(PreferencesData.MaxDistance);
        preferences.Gender.Should().Be(PreferencesData.Gender);
    }

    [Theory]
    [InlineData(-1, 30, 50, Gender.Female)]
    [InlineData(18, -1, 50, Gender.Male)]
    public void Create_ShouldThrowArgumentException_WhenAgeIsNegative(int minAge, int maxAge, int maxDistance,
        Gender gender)
    {
        // Act
        Action act = () => Preferences.Create(minAge, maxAge, maxDistance, gender);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Age cannot be negative");
    }

    [Fact]
    public void Create_ShouldThrowArgumentException_WhenMinAgeIsGreaterThanMaxAge()
    {
        // Act
        Action act = () => Preferences.Create(
            PreferencesData.MaxAge,
            PreferencesData.MinAge,
            PreferencesData.MaxDistance,
            PreferencesData.Gender);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Min age cannot be greater than max age");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public void Create_ShouldThrowArgumentException_WhenMaxDistanceIsOutOfRange(int maxDistance)
    {
        // Act
        Action act = () => Preferences.Create(
            PreferencesData.MinAge,
            PreferencesData.MaxAge,
            maxDistance,
            PreferencesData.Gender);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Max distance must be in range from 1 to 100");
    }
}