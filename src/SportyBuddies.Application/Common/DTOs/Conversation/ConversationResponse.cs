namespace SportyBuddies.Application.Common.DTOs.Conversation;

public record ConversationResponse(Guid Id, Guid CreatorId, List<ParticipantResponse> Participants);