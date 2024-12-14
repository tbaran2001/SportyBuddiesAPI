using Profile.Domain.Common;

namespace Profile.Domain.Events;

public class ProfileCreatedEvent(Models.Profile profile) : IDomainEvent
{

}