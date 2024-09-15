using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users.Events;

public record UserDeletedEvent(Guid UserId) : IDomainEvent;