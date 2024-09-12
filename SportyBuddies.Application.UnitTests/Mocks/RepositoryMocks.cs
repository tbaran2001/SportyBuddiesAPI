using Moq;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Mocks;

public class RepositoryMocks
{
    public static Mock<ISportsRepository> GetSportsRepository()
    {
        var sports = new List<Sport>
        {
            new("Football", "Football description", new List<User>()),
            new("Basketball", "Basketball description", new List<User>()),
            new("Tennis", "Tennis description", new List<User>())
        };

        var mockSportsRepository = new Mock<ISportsRepository>();

        mockSportsRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(sports);
        mockSportsRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(sports.FirstOrDefault(s => s.Id == id)));
        mockSportsRepository.Setup(repo => repo.AddAsync(It.IsAny<Sport>()))
            .Returns((Sport sport) =>
            {
                sports.Add(sport);
                return Task.CompletedTask;
            });
        mockSportsRepository.Setup(repo => repo.Update(It.IsAny<Sport>()))
            .Callback((Sport sport) =>
            {
                var existingSport = sports.FirstOrDefault(s => s.Id == sport.Id);
                if (existingSport != null)
                {
                    sports.Remove(existingSport);
                    sports.Add(sport);
                }
            });
        mockSportsRepository.Setup(repo => repo.Remove(It.IsAny<Sport>()))
            .Callback((Sport sport) => sports.Remove(sport));
        mockSportsRepository.Setup(repo => repo.SportExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => sports.Any(s => s.Id == id));

        return mockSportsRepository;
    }
}