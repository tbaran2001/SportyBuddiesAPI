using SportyBuddiesAPI.Models;

namespace SportyBuddiesAPI;

public class SportyBuddiesDataStore
{
    public List<UserDto> Users { get; set; }
    public List<SportDto> Sports { get; set; }
    public List<UserSportDto> UserSports { get; set; }
    public static SportyBuddiesDataStore Current { get; } = new SportyBuddiesDataStore();

    public SportyBuddiesDataStore()
    {
        Users = new List<UserDto>
        {
            new UserDto
            {
                Id = 1,
                Name = "John",
                Description = "John is a software developer"
            },
            new UserDto
            {
                Id = 2,
                Name = "Jane",
                Description = "Jane is a software developer"
            },
            new UserDto
            {
                Id = 3,
                Name = "Tom",
                Description = "Tom is a software developer"
            }
        };
        Sports = new List<SportDto>
        {
            new SportDto
            {
                Id = 1,
                Name = "Soccer",
                Description = "Soccer is a team sport"
            },
            new SportDto
            {
                Id = 2,
                Name = "Basketball",
                Description = "Basketball is a team sport"
            },
            new SportDto
            {
                Id = 3,
                Name = "Tennis",
                Description = "Tennis is a racket sport"
            }
        };
        UserSports = new List<UserSportDto>
        {
            new UserSportDto
            {
                UserId = 1,
                User = Users[0],
                SportId = 1,
                Sport = Sports[0]
            },
            new UserSportDto
            {
                UserId = 2,
                User = Users[1],
                SportId = 2,
                Sport = Sports[1]
            },
            new UserSportDto
            {
                UserId = 3,
                User = Users[2],
                SportId = 3,
                Sport = Sports[2]
            },
            new UserSportDto
            {
                UserId = 1,
                User = Users[0],
                SportId = 2,
                Sport = Sports[1]
            },
            new UserSportDto
            {
                UserId = 2,
                User = Users[1],
                SportId = 3,
                Sport = Sports[2]
            },
            new UserSportDto
            {
                UserId = 3,
                User = Users[2],
                SportId = 1,
                Sport = Sports[0]
            }
        };
    }
}