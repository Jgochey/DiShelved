using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class ContainerEndpoints
{
    public static void MapContainerEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get All Containers
        routes.MapGet("/Containers", async (IContainerService repo) =>
        {
            return await repo.GetAllContainersAsync();
        })
        .WithName("GetAllContainers")
        .Produces<List<Container>>(StatusCodes.Status200OK);

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
        routes.MapDelete("/Containers/{id}", async (int id, IContainerService repo) =>
        {
            var deletion = await repo.DeleteContainerAsync(id);
            return deletion ? Results.NoContent() : Results.NotFound();
            
        })
        .WithName("DeleteContainer")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
