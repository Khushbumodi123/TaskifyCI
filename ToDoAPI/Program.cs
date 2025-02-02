using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoAPI.AppDataContext;
using ToDoAPI.Models;
using ToDoAPI.Middleware;
using ToDoAPI.Services;
using ToDoAPI.Interfaces;


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

// Add services to the container.
builder.Services.AddControllers();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add ProblemDetails middleware
builder.Services.AddProblemDetails();

// Add logging
builder.Services.AddLogging();

// Register custom services
builder.Services.AddScoped<IToDoServices, ToDoServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Uncomment HTTPS redirection for production
// app.UseHttpsRedirection();

// Use custom exception handler
app.UseExceptionHandler("/error");

// Use routing
app.UseRouting();

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();
