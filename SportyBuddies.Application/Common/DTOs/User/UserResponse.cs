namespace SportyBuddies.Application.Common.DTOs.User;

public record UserResponse(Guid Id, string Name, string Description, DateTime LastActive);