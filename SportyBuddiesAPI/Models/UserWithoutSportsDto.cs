namespace SportyBuddiesAPI.Models;

public class UserWithoutSportsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
}