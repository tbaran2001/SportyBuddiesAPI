using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Common.DTOs.Buddy;

public record BuddyWithUsersResponse(Guid Id, UserResponse User, UserResponse MatchedUser, DateTime MatchDateTime);