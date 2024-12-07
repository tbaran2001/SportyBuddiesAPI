using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Identity;

namespace SportyBuddies.Api.FunctionalTests;

internal static class UserData
{
    public static CustomRegisterRequest RegisterTestUserRequest = new()
    {
        Email = "test@test.pl",
        Password = "Password123!",
        Name = "test",
        DateOfBirth = new DateOnly(1990, 1, 1),
        Gender = Gender.Male
    };
}