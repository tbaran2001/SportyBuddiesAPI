using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Domain.UserAggregate.Events;

public record UserDeletedEvent(UserId UserId) : IDomainEvent;