using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Constants;

namespace SportyBuddies.Application.UnitTests.Authentication;

public class UserContextTests
{
    [Fact]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // arrange
        var dateOfBirth = new DateOnly(1990, 1, 1);

        var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
        var userId = Guid.NewGuid().ToString();
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Email, "test@test.com"),
            new(ClaimTypes.Role, UserRoles.Admin),
            new(ClaimTypes.Role, UserRoles.User),
            new("Nationality", "German"),
            new("DateOfBirth", dateOfBirth.ToString("MM/dd/yyyy"))
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextAccessorMock.HttpContext.Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock);

        // act
        var currentUser = userContext.GetCurrentUser();


        // asset
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be(userId);
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
    }

    [Fact]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
    {
        // Arrange
        var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
        httpContextAccessorMock.HttpContext.Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock);

        // act
        Action action = () => userContext.GetCurrentUser();

        // assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("User context is not available");
    }

    [Fact]
    public void GetCurrentUser_WithUnauthenticatedUser_ThrowsUnauthorizedException()
    {
        // Arrange
        var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        httpContextAccessorMock.HttpContext.Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock);

        // Act
        Action action = () => userContext.GetCurrentUser();

        // Assert
        action.Should()
            .Throw<UnauthorizedException>()
            .WithMessage("User is not authenticated");
    }
}