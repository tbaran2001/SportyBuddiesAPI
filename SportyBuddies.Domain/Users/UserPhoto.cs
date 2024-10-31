using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users;

public class UserPhoto : Entity
{
    private UserPhoto(
        Guid id,
        User user,
        string url,
        bool isMain)
        : base(id)
    {
        User = user;
        Url = url;
        IsMain = isMain;
        UserId = user.Id;
    }

    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public string Url { get; private set; }
    public bool IsMain { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public static UserPhoto Create(User user, string url, bool isMain)
    {
        return new UserPhoto(
            id: Guid.NewGuid(),
            user: user,
            url: url,
            isMain: isMain);
    }

    private UserPhoto()
    {
    }
}