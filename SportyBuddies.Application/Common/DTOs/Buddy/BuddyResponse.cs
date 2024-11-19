using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Common.DTOs.Buddy;

public record BuddyResponse(Guid Id, UserResponse MatchedUser, DateTime MatchDateTime, ConversationResponse? Conversation);