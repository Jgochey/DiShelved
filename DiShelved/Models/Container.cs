namespace DiShelved.Models;

public class Container
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int LocationId { get; set; }
    public int UserId { get; set; }
    public string? Image { get; set; }
}
