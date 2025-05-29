using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        // Create User
        routes.MapPost("/Users", async (User User, IUserService repo) =>
        {
            var createdUser = await repo.CreateUserAsync(User); 
            return Results.Created($"/api/Users/{createdUser.Id}", createdUser);
        })
        .WithName("CreateUser")
        .Produces<User>(StatusCodes.Status201Created);

    }
}
