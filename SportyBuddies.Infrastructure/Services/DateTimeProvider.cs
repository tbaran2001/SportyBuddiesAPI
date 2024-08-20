using SportyBuddies.Application.Common.Interfaces.Services;

namespace SportyBuddies.Infrastructure.Services;

public class DateTimeProvider:IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}