using SportyBuddies.Domain.Entities;

namespace SportyBuddies.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);