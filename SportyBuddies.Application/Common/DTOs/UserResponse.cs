namespace SportyBuddies.Application.Common.DTOs;

public record UserResponse(Guid Id, string Name, string Description, DateTime LastActive);