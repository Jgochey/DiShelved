namespace DiShelved.Models;

// ItemCategory is a join table between Item and Category
// It contains the ItemId and CategoryId as foreign keys
// It is used to create a many-to-many relationship between Item and Category
// The composite key is used to ensure that the combination of ItemId and CategoryId is unique
public class ItemCategory
{
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public (int ItemId, int CategoryId) CompositeKey => (ItemId, CategoryId);    
}
