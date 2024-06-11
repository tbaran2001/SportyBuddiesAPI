namespace SportyBuddiesAPI.Models;

public class SportDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    public ICollection<UserSportDto> UserSports { get; set; } = new List<UserSportDto>();
}