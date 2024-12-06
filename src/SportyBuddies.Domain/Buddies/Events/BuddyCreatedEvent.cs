using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Buddies.Events;

public record BuddyCreatedEvent(Guid UserId, Guid MatchedUserId, DateTime MatchDateTime) : IDomainEvent;