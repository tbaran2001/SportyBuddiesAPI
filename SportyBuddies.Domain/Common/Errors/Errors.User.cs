using ErrorOr;

namespace SportyBuddies.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplcateEmail => Error.Conflict(code: "User.DuplicateEmail",
            description: "User with this email already exists");
    }
}