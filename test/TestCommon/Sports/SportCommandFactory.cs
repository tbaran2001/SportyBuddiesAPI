using SportyBuddies.Application.Features.Sports.Commands.CreateSport;
using TestCommon.TestConstants;

namespace TestCommon.Sports;

public static class SportCommandFactory
{
    public static CreateSportCommand CreateCreateSportCommand(string name = Constants.Sport.Name,
        string description = Constants.Sport.Description)
    {
        return new CreateSportCommand(name, description);
    }
}