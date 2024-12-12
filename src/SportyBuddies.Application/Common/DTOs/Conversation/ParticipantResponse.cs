using SportyBuddies.Application.Common.DTOs.Profile;

namespace SportyBuddies.Application.Common.DTOs.Conversation;

public record ParticipantResponse(Guid Id, Guid ConversationId, ProfileResponse Profile, DateTime CreatedAt);