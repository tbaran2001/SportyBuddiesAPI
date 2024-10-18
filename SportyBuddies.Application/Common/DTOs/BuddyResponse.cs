namespace SportyBuddies.Application.Common.DTOs;

public record BuddyResponse(Guid Id, UserWithSportsResponse UserWithSports, UserWithSportsResponse MatchedUserWithSports, DateTime MatchDateTime);