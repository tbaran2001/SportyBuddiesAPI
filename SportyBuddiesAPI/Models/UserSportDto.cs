namespace SportyBuddiesAPI.Models;

public class UserSportDto
{
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
    public int SportId { get; set; }
    public SportDto Sport { get; set; } = null!;
}