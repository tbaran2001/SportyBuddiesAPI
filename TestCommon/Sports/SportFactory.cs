using SportyBuddies.Application.Common.DTOs.Sport;
using TestCommon.TestConstants;

namespace TestCommon.Sports;

public static class SportFactory
{
    public static SportResponse CreateSport(
        string name = Constants.Sport.Name,
        string description = Constants.Sport.Description,
        Guid? id = null)
    {
        return new SportResponse(
            id ?? Constants.Sport.Id,
            name,
            description);
    }
}