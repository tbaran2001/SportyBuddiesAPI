using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestData;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class UpdatePreferencesTests
{
    [Fact]
    public void UpdatePreferences_Should_SetPreferencesPropertyValue()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var preferences = Preferences.Create(
            PreferencesData.MinAge,
            PreferencesData.MaxAge,
            PreferencesData.MaxDistance,
            PreferencesData.Gender);

        // Act
        user.UpdatePreferences(preferences);

        // Assert
        user.Preferences.Should().Be(preferences);
    }
}