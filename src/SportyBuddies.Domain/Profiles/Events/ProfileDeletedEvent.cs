using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Profiles.Events;

public record ProfileDeletedEvent(Guid ProfileId) : IDomainEvent;