using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Common.Interfaces;

public interface ISportsRepository
{
    Task<IEnumerable<Sport>> GetAllSportsAsync();
    Task<Sport?> GetSportByIdAsync(Guid sportId);
    Task AddSportAsync(Sport sport);
    void RemoveSport(Sport sport);
}