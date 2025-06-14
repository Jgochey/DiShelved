using System;
using DiShelved.Models;

namespace DiShelved.DTOs;

public class ItemWithCategoriesDTO
{
  public Item Item { get; set; }
  public List<Category> Categories { get; set; }
}
