namespace SportyBuddies.Application.Common.DTOs;

public record MessageResponse(Guid Id, Guid SenderId, Guid RecipientId, string Content, DateTime TimeSent);