using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestConstants;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class CreatePreferencesTests
{
    [Fact]
    public void Create_ShouldReturnPreferences_WhenValidValues()
    {
        // Act
        var preferences = Preferences.Create(PreferencesConstants.MinAge, PreferencesConstants.MaxAge,
            PreferencesConstants.Gender);

        // Assert
        preferences.MinAge.Should().Be(PreferencesConstants.MinAge);
        preferences.MaxAge.Should().Be(PreferencesConstants.MaxAge);
        preferences.Gender.Should().Be(PreferencesConstants.Gender);
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
        Action act = () => Preferences.Create(PreferencesConstants.MaxAge, PreferencesConstants.MinAge,
            PreferencesConstants.Gender);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Min age cannot be greater than max age");
    }
}