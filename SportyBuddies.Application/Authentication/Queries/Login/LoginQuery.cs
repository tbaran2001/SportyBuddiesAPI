using MediatR;
using ErrorOr;
using SportyBuddies.Application.Authentication.Common;

namespace SportyBuddies.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;