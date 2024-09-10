namespace SportyBuddies.Contracts.Matches;

public record MatchResponse(Guid Id, Guid UserId, Guid MatchedUserId, Swipe Swipe, DateTime SwipeDateTime);