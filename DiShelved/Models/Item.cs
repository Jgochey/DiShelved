namespace DiShelved.Models;

public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int ContainerId { get; set; }
    public int Quantity { get; set; }
    public bool Complete { get; set; }
    public int UserId { get; set; }
    public string? Image { get; set; }
}
