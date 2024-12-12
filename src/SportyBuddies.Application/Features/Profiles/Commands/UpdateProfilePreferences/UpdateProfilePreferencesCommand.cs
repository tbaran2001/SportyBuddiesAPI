using MediatR;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.Features.Profiles.Commands.UpdateProfilePreferences;

public record UpdateProfilePreferencesCommand(int MinAge, int MaxAge, int MaxDistance, Gender Gender)
    : IRequest;