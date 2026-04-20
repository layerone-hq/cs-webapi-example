using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApiDemo.Application.Interfaces;
using WebApiDemo.Application.Services;
using WebApiDemo.Domain.Repositories;
using WebApiDemo.Infrastructure.Persistence;
using WebApiDemo.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("WebApiDemo.Infrastructure")));

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IToDoService, ToDoService>();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
