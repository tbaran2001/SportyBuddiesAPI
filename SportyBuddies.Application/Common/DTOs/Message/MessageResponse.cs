namespace SportyBuddies.Application.Common.DTOs.Message;

public record MessageResponse(Guid Id, Guid SenderId, Guid RecipientId, string Content, DateTime TimeSent);