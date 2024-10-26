using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.TestConstants;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.TestUtils.Users;

public static class UserFactory
{
    public static User Create(
        string? name = null,
        string? description = null,
        DateTime? lastActive = null,
        List<Sport>? sports = null,
        UserPhoto? mainPhoto = null,
        List<UserPhoto>? photos = null,
        Gender? gender = null,
        DateTime? dateOfBirth = null,
        Guid? id = null)
    {
        return new User(
            name ?? UserConstants.Name,
            description ?? UserConstants.Description,
            lastActive ?? UserConstants.LastActive,
            sports ?? UserConstants.Sports,
            mainPhoto ?? UserConstants.MainPhoto,
            photos ?? UserConstants.Photos,
            dateOfBirth ?? UserConstants.DateOfBirth,
            gender ?? UserConstants.Gender,
            id ?? UserConstants.Id);
    }
}