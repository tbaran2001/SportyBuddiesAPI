namespace SportyBuddies.Contracts.Sports;

public record CreateSportRequest(SportType SportType, string Name, string Description, Guid AdminId);