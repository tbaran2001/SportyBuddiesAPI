using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Common.DTOs.Profile;

namespace SportyBuddies.Application.Common.DTOs.Buddy;

public record BuddyResponse(Guid Id,Guid OppositeBuddyId, ProfileResponse MatchedProfile, DateTime CreatedOnUtc, ConversationResponse? Conversation);