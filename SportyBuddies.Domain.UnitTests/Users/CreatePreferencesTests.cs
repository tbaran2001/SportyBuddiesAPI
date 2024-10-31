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
        var preferences = Preferences.Create(PreferencesData.MinAge, PreferencesData.MaxAge,
            PreferencesData.Gender);

        // Assert
        preferences.MinAge.Should().Be(PreferencesData.MinAge);
        preferences.MaxAge.Should().Be(PreferencesData.MaxAge);
        preferences.Gender.Should().Be(PreferencesData.Gender);
    }

    [Theory]
    [InlineData(-1, 30, Gender.Female)]
    [InlineData(18, -1, Gender.Male)]
    public void Create_ShouldThrowArgumentException_WhenAgeIsNegative(int minAge, int maxAge, Gender gender)
    {
        // Act
        Action act = () => Preferences.Create(minAge, maxAge, gender);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Age cannot be negative");
    }

    [Fact]
    public void Create_ShouldThrowArgumentException_WhenMinAgeIsGreaterThanMaxAge()
    {
        // Act
        Action act = () => Preferences.Create(PreferencesData.MaxAge, PreferencesData.MinAge,
            PreferencesData.Gender);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Min age cannot be greater than max age");
    }
}