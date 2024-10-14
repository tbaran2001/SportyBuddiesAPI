﻿namespace SportyBuddies.Application.Common.DTOs;

public record UserWithSportsResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime LastActive,
    List<SportResponse> Sports);