using Microsoft.Build.Framework;

namespace SportyBuddiesAPI.Models;

public class MatchForUpdateDto
{
    [Required]
    public Swipe? Swipe { get; set; }
    [Required]
    public DateTime? SwipeDateTime { get; set; }
}