using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SportyBuddiesAPI.Models;

namespace SportyBuddiesAPI.Entities;

public class Match
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public User User { get; set; }
    public User MatchedUser { get; set; }
    public DateTime MatchDateTime { get; set; }
    public Swipe? Swipe { get; set; }
    public DateTime? SwipeDateTime { get; set; }
}