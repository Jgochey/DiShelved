using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get All Items
        routes.MapGet("/Items", async (IItemService repo) =>
        {
            return await repo.GetAllItemsAsync();
        })
        .WithName("GetAllItems")
        .Produces<List<Item>>(StatusCodes.Status200OK);

        // Get Item By Id
        routes.MapGet("/Items/{id}", async (int id, IItemService repo) =>
        {
            var Item = await repo.GetItemByIdAsync(id);
            return Item is not null ? Results.Ok(Item) : Results.NotFound();
        })
        .WithName("GetItemById")
        .Produces<Item>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Create Item
        routes.MapPost("/Items", async (Item Item, IItemService repo) =>
        {
            var createdItem = await repo.CreateItemAsync(Item); 
            return Results.Created($"/api/Items/{createdItem.Id}", createdItem);
        })
        .WithName("CreateItem")
        .Produces<Item>(StatusCodes.Status201Created);

        // Update Item
        routes.MapPut("/Items/{id}", async (int id, Item Item, IItemService repo) =>
        {
            var updatedItem = await repo.UpdateItemAsync(id, Item);
            return updatedItem is not null ? Results.Ok(updatedItem) : Results.NotFound();
        })
        .WithName("UpdateItem")
        .Produces<Item>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Delete Item
        routes.MapDelete("/Items/{id}", async (int id, IItemService repo) =>
        {
            var deletion = await repo.DeleteItemAsync(id);
            return deletion ? Results.NoContent() : Results.NotFound();
            
        })
        .WithName("DeleteItem")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
