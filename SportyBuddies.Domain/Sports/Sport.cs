namespace SportyBuddies.Domain.Sports;

public class Sport
{
    public Guid Id { get; set; }
    public string SportType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}