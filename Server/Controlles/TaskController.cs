using Microsoft.AspNetCore.Mvc;
using SharedProject.Interfaces;
using SharedProject.Models;

namespace Server.Controlles;

public static class TaskController
{
    public static RouteGroupBuilder AddTaskController(this WebApplication application)
    {
        var group = application.MapGroup("/api/task");

        group.MapGet("/list", async ([FromServices] ITaskService taskService) =>
        {
            try
            {
                var result = await taskService.GetAll();
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Operazione conclusa con un erore.\n{ex.Message}");
            }
        });

        group.MapPost("/save", async ([FromBody] TaskItem item, [FromServices] ITaskService taskService) =>
        {
            try
            {
                var result = await taskService.Save(item);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Operazione conclusa con un erore.\n{ex.Message}");
            }
        });

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
