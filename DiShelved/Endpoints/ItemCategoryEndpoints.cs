using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class ItemCategoryEndpoints
{
    public static void MapItemCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        // Create ItemCategory
        routes.MapPost("/ItemCategory/{itemId}/{categoryId}", async (int itemId, int categoryId, IItemCategoryService repo) =>
        {
            var createdItemCategory = await repo.CreateItemCategoryAsync(new ItemCategory
            {
                ItemId = itemId,
                CategoryId = categoryId
            });
            return Results.Created($"/api/ItemCategory/{itemId}/{categoryId}", createdItemCategory);
        })
        .WithName("CreateItemCategory")
        .Produces<ItemCategory>(StatusCodes.Status201Created);

        // Delete ItemCategory
        routes.MapDelete("/ItemCategory/{itemId}/{categoryId}", async (int itemId, int categoryId, IItemCategoryService repo) =>
        {
            var deletion = await repo.DeleteItemCategoryAsync(itemId, categoryId);
            return deletion ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteItemCategory")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
        
        // Update ItemCategory
        // routes.MapPut("/ItemCategory/{itemId}/{categoryId}", async (int itemId, int categoryId, ItemCategory itemCategory, IItemCategoryService repo) =>
        // {
        //     var updatedItemCategory = await repo.UpdateItemCategoryAsync(itemId, categoryId, itemCategory);
        //     return updatedItemCategory is not null ? Results.Ok(updatedItemCategory) : Results.NotFound();
        // });
    }
}
