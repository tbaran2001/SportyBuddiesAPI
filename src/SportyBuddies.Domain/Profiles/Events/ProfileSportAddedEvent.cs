using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Profiles.Events;

public record ProfileSportAddedEvent(Guid ProfileId,Guid SportId):IDomainEvent;