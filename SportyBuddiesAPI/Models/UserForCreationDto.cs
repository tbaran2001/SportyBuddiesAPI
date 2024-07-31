using System.ComponentModel.DataAnnotations;

namespace SportyBuddiesAPI.Models;

public class UserForCreationDto
{
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;
    [MaxLength(200)]
    public string? Description { get; set; }
}