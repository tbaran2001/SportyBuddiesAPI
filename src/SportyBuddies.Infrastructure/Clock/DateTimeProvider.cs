using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Infrastructure.Clock;

public class DateTimeProvider:IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}