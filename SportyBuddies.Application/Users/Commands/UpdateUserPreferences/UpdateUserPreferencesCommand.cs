using ErrorOr;
using MediatR;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUserPreferences;

public record UpdateUserPreferencesCommand(Guid UserId, int MinAge, int MaxAge, Gender Gender)
    : IRequest<ErrorOr<Success>>;