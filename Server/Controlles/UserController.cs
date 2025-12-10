using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using SharedProject.Interfaces;
using SharedProject.Models;
using System.Security.Claims;

namespace Server.Controlles;

public static class UserController
{
    public static RouteGroupBuilder AddUserController(this WebApplication application)
    {
        var group = application.MapGroup("/api/user");

        group.MapPost("/login", async ([FromForm] string username, [FromForm] string password, [FromServices] IUserRepository userRepository) =>
        {
            try
            {
                var user = await userRepository.Get(username);
                if (user == null)
                    return Results.NotFound();

                if (user.Password != password)
                    return Results.Unauthorized();


                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Operazione conclusa con un erore.\n{ex.Message}");
            }
        }).DisableAntiforgery();

        group.MapDelete("/item/{id}", async ([FromRoute] string id, [FromServices] ITaskService taskService) =>
        {
            try
            {
                var result = await taskService.Delete(id);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Operazione conclusa con un erore.\n{ex.Message}");
            }
        });

        return group;
    }
}
