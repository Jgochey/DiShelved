using System;
using DiShelved.Models;
namespace DiShelved.DTOs;

// This will be used for Moving Items between Containers. As such, it should not require all the fields that an UpdateItem call would.
public class MoveItemDTO
{
    public int Id { get; set; }
    public int ContainerId { get; set; }

}
