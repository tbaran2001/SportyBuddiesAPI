namespace SportyBuddies.Application.Common.DTOs;

public record BuddyResponse(Guid Id, Guid UserId, Guid MatchedUserId, DateTime MatchDateTime);