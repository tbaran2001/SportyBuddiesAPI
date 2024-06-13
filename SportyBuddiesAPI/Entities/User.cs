using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportyBuddiesAPI.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    public ICollection<Sport> Sports { get; set; } = new List<Sport>();

}