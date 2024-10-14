using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Application.Common.Interfaces;

public interface ISportsRepository
{
    Task<IEnumerable<Sport>> GetAllSportsAsync();
    Task<Sport?> GetSportByIdAsync(SportId sportId);
    Task AddSportAsync(Sport sport);
    void RemoveSport(Sport sport);
}