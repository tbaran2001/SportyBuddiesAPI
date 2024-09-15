using SportyBuddies.Application.Common.DTOs;
using TestCommon.TestConstants;

namespace TestCommon.Sports;

public static class SportFactory
{
    public static SportDto CreateSport(
        string name = Constants.Sport.Name,
        string description = Constants.Sport.Description,
        Guid? id = null)
    {
        return new SportDto(
            id ?? Constants.Sport.Id,
            name,
            description);
    }
}