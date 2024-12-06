using Microsoft.AspNetCore.Authorization;
using SportyBuddies.Application.Authentication;

namespace SportyBuddies.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser.DateOfBirth == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            context.Succeed(requirement);
        else
            context.Fail();

        return Task.CompletedTask;
    }
}