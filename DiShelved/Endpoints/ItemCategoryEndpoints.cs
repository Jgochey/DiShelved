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

        // Get ItemCategory By Id
        routes.MapGet("/ItemCategory/{itemId}/{categoryId}", async (int itemId, int categoryId, IItemCategoryService repo) =>
        {
            var itemCategory = await repo.GetItemCategoryByIdAsync(itemId, categoryId);
            return itemCategory is not null ? Results.Ok(itemCategory) : Results.NotFound();
        })
         .WithName("GetItemCategoryById")
         .Produces<ItemCategory>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound);

        // Get ItemCategories By Item Id
        routes.MapGet("/ItemCategory/Item/{itemId}", async (int itemId, IItemCategoryService repo) =>
        {
            var itemCategories = await repo.GetItemCategoriesByItemIdAsync(itemId);
            return itemCategories is not null ? Results.Ok(itemCategories) : Results.NotFound();
        })
         .WithName("GetItemCategoriesByItemId")
         .Produces<IEnumerable<ItemCategory>>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound);

        // Get ItemCategories By Category Id
        routes.MapGet("/ItemCategory/Category/{categoryId}", async (int categoryId, IItemCategoryService repo) =>
        {
            var itemCategories = await repo.GetItemCategoriesByCategoryIdAsync(categoryId);
            return itemCategories is not null ? Results.Ok(itemCategories) : Results.NotFound();
        })
         .WithName("GetItemCategoriesByCategoryId")
         .Produces<IEnumerable<ItemCategory>>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound);

        // Update ItemCategory
        // routes.MapPut("/ItemCategory/{itemId}/{categoryId}", async (int itemId, int categoryId, ItemCategory itemCategory, IItemCategoryService repo) =>
        // {
        //     var updatedItemCategory = await repo.UpdateItemCategoryAsync(itemId, categoryId, itemCategory);
        //     return updatedItemCategory is not null ? Results.Ok(updatedItemCategory) : Results.NotFound();
        // });
    }
}
