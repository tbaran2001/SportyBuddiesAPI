using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Infrastructure.Common.Clock;

public class DateTimeProvider:IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}