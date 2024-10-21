namespace SportyBuddies.Application.Common.DTOs;

public record BuddyWithUsersResponse(Guid Id, UserResponse User, UserResponse MatchedUser, DateTime MatchDateTime);