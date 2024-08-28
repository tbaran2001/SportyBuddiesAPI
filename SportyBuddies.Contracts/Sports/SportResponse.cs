namespace SportyBuddies.Contracts.Sports;

public record SportResponse(Guid Id, SportType SportType, string Name, string Description);