using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get Categories By User Id
        routes.MapGet("/Categories/User/{userId}", async (int userId, ICategoryService repo) =>
        {
            var categories = await repo.GetCategoriesByUserIdAsync(userId);
            return categories is not null ? Results.Ok(categories) : Results.NotFound();
        })
        .WithName("GetCategoriesByUserId")
        .Produces<List<Category>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Get Categories By User Uid
        routes.MapGet("/Categories/UserUid/{uid}", async (string uid, ICategoryService repo) =>
        {
            try
            {
                var categories = await repo.GetCategoriesByUserUidAsync(uid);
                // Always return 200 with a list (empty or not)
                return Results.Ok(categories ?? new List<Category>());
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return Results.Problem("An error occurred while fetching categories.");
            }
        })
        .WithName("GetCategoriesByUserUid")
        .Produces<List<Category>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        // Get Category By Id
        routes.MapGet("/Categories/{id}", async (int id, ICategoryService repo) =>
        {
            var Category = await repo.GetCategoryByIdAsync(id);
            return Category is not null ? Results.Ok(Category) : Results.NotFound();
        })
        .WithName("GetCategoryById")
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Create Category
        routes.MapPost("/Categories", async (Category Category, ICategoryService repo) =>
        {
            var createdCategory = await repo.CreateCategoryAsync(Category); 
            return Results.Created($"/api/Categories/{createdCategory.Id}", createdCategory);
        })
        .WithName("CreateCategory")
        .Produces<Category>(StatusCodes.Status201Created);

        // Update Category
        routes.MapPut("/Categories/{id}", async (int id, Category Category, ICategoryService repo) =>
        {
            var updatedCategory = await repo.UpdateCategoryAsync(id, Category);
            return updatedCategory is not null ? Results.Ok(updatedCategory) : Results.NotFound();
        })
        .WithName("UpdateCategory")
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Delete Category
        routes.MapDelete("/Categories/{id}", async (int id, ICategoryService repo, IItemCategoryService itemCategoryService) =>
        {
            // Check for associated items via ItemCategory
            var itemCategories = await itemCategoryService.GetItemCategoriesByCategoryIdAsync(id);
            if (itemCategories != null && itemCategories.Any())
            {
                return Results.BadRequest(new { message = "Category has Items associated with it. Remove any Items from this Category before deleting." });
            }

            var deletion = await repo.DeleteCategoryAsync(id);
            return deletion ? Results.NoContent() : Results.NotFound();
            
        })
        .WithName("DeleteCategory")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
