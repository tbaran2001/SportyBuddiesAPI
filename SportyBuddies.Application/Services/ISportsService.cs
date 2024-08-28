namespace SportyBuddies.Application.Services;

public interface ISportsService
{
    Guid CreateSport(string sportType, string name, string description, Guid adminId);
}