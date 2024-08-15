namespace SportyBuddiesAPI.Models;

public class UserCurrentDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    public DateTime LastActive { get; set; } = DateTime.Now;
    public ICollection<SportDto> Sports { get; set; } = new List<SportDto>();
}