using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Server.Controlles;
using Server.Interfaces;
using Server.Repositories;
using Server.Services;
using SharedProject.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    var dbName = "database.sqlite3";
    bool newDb = File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbName)) == false;

    options.UseSqlite($"Data Source={dbName};");
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

var host = builder.Build();

host.AddUserController();
host.AddTaskController();

host.Run();