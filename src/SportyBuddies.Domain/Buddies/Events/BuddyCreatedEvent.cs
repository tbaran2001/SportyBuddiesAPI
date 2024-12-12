using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Buddies.Events;

public record BuddyCreatedEvent(Guid ProfileId, Guid MatchedProfileId, DateTime MatchDateTime) : IDomainEvent;