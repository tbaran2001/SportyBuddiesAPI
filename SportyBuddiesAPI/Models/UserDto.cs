namespace SportyBuddiesAPI.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    public ICollection<SportDto> Sports { get; set; } = new List<SportDto>();
}