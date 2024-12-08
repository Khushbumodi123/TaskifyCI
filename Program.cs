using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoAPI.AppDataContext;
using ToDoAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DbSettings and configure TodoDbContext
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddDbContext<TodoDbContext>((serviceProvider, options) =>
{
    var dbSettings = serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value;
    options.UseMySql(dbSettings.ConnectionString, ServerVersion.AutoDetect(dbSettings.ConnectionString));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
