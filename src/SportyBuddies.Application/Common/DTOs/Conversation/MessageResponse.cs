namespace SportyBuddies.Application.Common.DTOs.Conversation;

public record MessageResponse(Guid Id, Guid ConversationId,Guid SenderId, string Content, DateTime CreatedOnUtc);