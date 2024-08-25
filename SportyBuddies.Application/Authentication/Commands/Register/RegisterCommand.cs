using MediatR;
using ErrorOr;
using SportyBuddies.Application.Authentication.Common;

namespace SportyBuddies.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;