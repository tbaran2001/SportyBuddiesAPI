using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Infrastructure;

namespace SportyBuddies.Api.FunctionalTests;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    AuthClaimsProvider claimsProvider,
    SportyBuddiesDbContext dbContext) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string SchemeName = "Test";
    private readonly IList<Claim> _claims = claimsProvider.Claims;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var currentUser = Profile.Create(ProfileData.TestUserId,"TestUser",new DateOnly(2000,1,1),Gender.Male);
        await dbContext.Profiles.AddAsync(currentUser);
        await dbContext.SaveChangesAsync();

        var identity = new ClaimsIdentity(_claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        var result = AuthenticateResult.Success(ticket);

        return result;
    }
}