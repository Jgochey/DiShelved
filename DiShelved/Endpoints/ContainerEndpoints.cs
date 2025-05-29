using DiShelved.Interfaces;
using DiShelved.Models;
using DiShelved.Services;

namespace DiShelved.Endpoints;

public static class ContainerEndpoints
{
    public static void MapContainerEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get Containers By User Id
        routes.MapGet("/Containers/User/{userId}", async (int userId, IContainerService repo) =>
        {
            var containers = await repo.GetContainersByUserIdAsync(userId);
            return containers is not null ? Results.Ok(containers) : Results.NotFound();
        })
        .WithName("GetContainersByUserId")
        .Produces<List<Container>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Get Container By Id
        routes.MapGet("/Containers/{id}", async (int id, IContainerService repo) =>
        {
            var Container = await repo.GetContainerByIdAsync(id);
            return Container is not null ? Results.Ok(Container) : Results.NotFound();
        })
        .WithName("GetContainerById")
        .Produces<Container>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Create Container
        routes.MapPost("/Containers", async (Container Container, IContainerService repo) =>
        {
            var createdContainer = await repo.CreateContainerAsync(Container);
            return Results.Created($"/api/Containers/{createdContainer.Id}", createdContainer);
        })
        .WithName("CreateContainer")
        .Produces<Container>(StatusCodes.Status201Created);

        // Update Container
        routes.MapPut("/Containers/{id}", async (int id, Container Container, IContainerService repo) =>
        {
            var updatedContainer = await repo.UpdateContainerAsync(id, Container);
            return updatedContainer is not null ? Results.Ok(updatedContainer) : Results.NotFound();
        })
        .WithName("UpdateContainer")
        .Produces<Container>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Delete Container
        routes.MapDelete("/Containers/{id}", async (int id, IContainerService repo, IItemService itemService) =>
        {
            var items = await itemService.GetItemsByContainerIdAsync(id);
            if (items.Any())
            {
                return Results.Problem("Container has Items associated with it. Remove any Items before deleting the Container.");
            }

            var deletion = await repo.DeleteContainerAsync(id);
            return deletion ? Results.NoContent() : Results.NotFound();

        })
        .WithName("DeleteContainer")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        // Get Containers By Location Id
        routes.MapGet("/Containers/Location/{locationId}", async (int locationId, IContainerService repo) =>
        {
            var containers = await repo.GetContainersByLocationIdAsync(locationId);
            return containers is not null ? Results.Ok(containers) : Results.NotFound();
        })
        .WithName("GetContainersByLocationId")
        .Produces<List<Container>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
