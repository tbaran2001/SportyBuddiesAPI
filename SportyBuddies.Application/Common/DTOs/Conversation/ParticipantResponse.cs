using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Common.DTOs.Conversation;

public record ParticipantResponse(Guid Id, Guid ConversationId, UserResponse User, DateTime CreatedAt);