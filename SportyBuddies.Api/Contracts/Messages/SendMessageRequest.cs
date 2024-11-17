namespace SportyBuddies.Api.Contracts.Messages;

public record SendMessageRequest(Guid ConversationId, string Content);