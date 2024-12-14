using Profile.Domain.Common;

namespace Profile.Domain.Events;

public class ProfileUpdatedEvent(Models.Profile profile) : IDomainEvent
{

}