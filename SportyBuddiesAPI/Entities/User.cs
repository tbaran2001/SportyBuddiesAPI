using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportyBuddiesAPI.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;
    [MaxLength(200)]
    public string? Description { get; set; }
    public DateTime LastActive { get; set; } = DateTime.Now;
    public ICollection<Sport> Sports { get; set; } = new List<Sport>();

}