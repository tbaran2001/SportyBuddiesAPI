using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Infrastructure.Identity;

namespace SportyBuddies.Infrastructure.Authorization;

public class ApplicationUserClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IOptions<IdentityOptions> options,
    IUsersRepository usersRepository)
    : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole<Guid>>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var claimsIdentity = await GenerateClaimsAsync(user);
        var dbUser = await usersRepository.GetUserByIdAsync(user.Id);

        claimsIdentity.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.ToString()));

        if(dbUser?.Description != null)
            claimsIdentity.AddClaim(new Claim(AppClaimTypes.Description, dbUser.Description));

        return new ClaimsPrincipal(claimsIdentity);
    }
}