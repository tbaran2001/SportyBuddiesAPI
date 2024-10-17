namespace SportyBuddies.Application.Hubs;

public record HubMessage(Guid Id, Guid SenderId, Guid RecipientId, string Content, DateTime TimeSent);