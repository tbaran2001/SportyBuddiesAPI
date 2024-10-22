namespace SportyBuddies.Application.Common.DTOs.Buddy;

public record BuddyResponse(Guid Id, Guid UserId, Guid MatchedUserId, DateTime MatchDateTime);