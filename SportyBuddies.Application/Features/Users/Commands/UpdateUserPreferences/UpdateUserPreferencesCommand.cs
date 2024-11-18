﻿using MediatR;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Users.Commands.UpdateUserPreferences;

public record UpdateUserPreferencesCommand(Guid UserId, int MinAge, int MaxAge, int MaxDistance, Gender Gender)
    : IRequest;