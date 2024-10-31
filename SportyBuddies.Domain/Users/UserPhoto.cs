using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users;

public class UserPhoto : Entity
{
    private UserPhoto(User user, string url, bool isMain, Guid? id = null) : base(id ?? Guid.NewGuid())
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

    private UserPhoto()
    {
    }
}