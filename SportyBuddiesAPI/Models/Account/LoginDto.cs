using System.ComponentModel.DataAnnotations;

namespace SportyBuddiesAPI.Models.Account;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}