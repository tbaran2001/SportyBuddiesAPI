using FluentAssertions;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Domain.Constants;

namespace SportyBuddies.Application.UnitTests.Authentication;

public class CurrentUserTests
{

    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "test@test.com", [UserRoles.Admin, UserRoles.User], null);

        // act
        var isInRole = currentUser.IsInRole(roleName);

        // assert
        isInRole.Should().BeTrue();
    }


    [Fact]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "test@test.com", [UserRoles.Admin, UserRoles.User], null);

        // act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        // assert
        isInRole.Should().BeFalse();

    }

    [Fact]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "test@test.com", [UserRoles.Admin, UserRoles.User], null);

        // act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // assert
        isInRole.Should().BeFalse();

    }
}