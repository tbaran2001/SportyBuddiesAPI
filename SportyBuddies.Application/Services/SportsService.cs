namespace SportyBuddies.Application.Services;

public class SportsService : ISportsService
{
    public Guid CreateSport(string sportType, string name, string description, Guid adminId)
    {
        return Guid.NewGuid();
    }
}