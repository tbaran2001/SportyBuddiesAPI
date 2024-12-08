using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportyBuddies.Domain.Users;
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

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var currentUser = User.Create(UserData.TestUserId,"TestUser",new DateOnly(2000,1,1),Gender.Male);
        dbContext.Users.Add(currentUser);
        dbContext.SaveChanges();

        var identity = new ClaimsIdentity(_claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}