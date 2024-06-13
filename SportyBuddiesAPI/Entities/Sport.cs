using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportyBuddiesAPI.Entities;

public class Sport
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    
}