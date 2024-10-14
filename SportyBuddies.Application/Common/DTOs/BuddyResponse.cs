namespace SportyBuddies.Application.Common.DTOs;

public record BuddyResponse(Guid Id, UserResponse User, UserResponse MatchedUser, DateTime MatchDateTime);