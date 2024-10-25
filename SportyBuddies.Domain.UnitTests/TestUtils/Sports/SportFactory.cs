using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.TestConstants;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.TestUtils.Sports;

public static class SportFactory
{
    public static Sport Create(
        string? name = null,
        string? description = null,
        List<User>? users = null,
        Guid? id = null)
    {
        return new Sport(
            name ?? SportConstants.Name,
            description ?? SportConstants.Description,
            users ?? SportConstants.Users,
            id ?? SportConstants.Id);
    }
}