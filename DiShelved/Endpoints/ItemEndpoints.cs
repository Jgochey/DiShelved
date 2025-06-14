using DiShelved.Interfaces;
using DiShelved.Models;
using DiShelved.DTOs;

namespace DiShelved.Endpoints;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get Items By User Id
        routes.MapGet("/Items/User/{userId}", async (int userId, IItemService repo) =>
        {
            var items = await repo.GetItemsByUserIdAsync(userId);
            return items is not null ? Results.Ok(items) : Results.NotFound();
        });

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

        // Move Item DTO
        routes.MapPut("/Items/Move/{id}", async (int id, MoveItemDTO moveItemDTO, IItemService repo) =>
        {
            if (moveItemDTO.ContainerId <= 0)
            {
                return Results.BadRequest("Invalid Container Id");
            }

            var movedItem = await repo.MoveItemAsync(id, moveItemDTO.ContainerId);
            return movedItem is not null ? Results.Ok(movedItem) : Results.NotFound();
        });

        // Search Items
        routes.MapGet("/Items/{userId}/Search/{query}", async (
            string query, 
            int userId, 
            IItemService itemService) =>
        {
            if (userId <= 0)
                return Results.BadRequest("Invalid User Id");

            if (string.IsNullOrWhiteSpace(query))
                return Results.BadRequest("Search query cannot be empty");

            var results = await itemService.SearchItemsAsync(query, userId);

            if (results == null || !results.Any())
                return Results.NotFound("No Items Found matching the search query");

            return Results.Ok(results);
        });
        
        // Get Items By Container Id
        routes.MapGet("/Items/Container/{containerId}", async (int containerId, IItemService repo) =>
        {
            if (containerId <= 0)
            {
                return Results.BadRequest("Invalid Container Id");
            }

            var items = await repo.GetItemsByContainerIdAsync(containerId);
            return items is not null ? Results.Ok(items) : Results.NotFound();
        });
    }
}
