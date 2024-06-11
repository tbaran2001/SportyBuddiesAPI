namespace SportyBuddiesAPI.Entities;

public class UserSport
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int SportId { get; set; }
    public Sport Sport { get; set; } = null!;
}