namespace SportyBuddies.Application.Common.DTOs.Message;

public record MessageResponse(Guid Id, Guid ConversationId,Guid SenderId, string Content, DateTime CreatedAt);