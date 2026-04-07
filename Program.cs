using Microsoft.EntityFrameworkCore; // Required for EF Core database context and configuration
using StudentEnrollment.API.Data;     // Your project namespace containing AppDbContext

var builder = WebApplication.CreateBuilder(args); // Creates a builder for configuring services and middleware

// Add services to the container (dependency injection)
builder.Services.AddControllers();               // Registers API controllers so they can handle HTTP requests
builder.Services.AddEndpointsApiExplorer();     // Scans controllers/endpoints to generate metadata for OpenAPI/Swagger

// Registers Swagger generator to produce OpenAPI JSON (swagger.json)
builder.Services.AddSwaggerGen(opt =>
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Student Enrollment API",
        Version = "v1",
        Description = "Handles students, courses, and enrollment."
    })
);               

builder.Services.AddDbContext<AppDbContext>(options =>  // Registers your EF Core database context
    // Uses SQLite database with connection string from appsettings
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")) 
);

var app = builder.Build(); // Builds the WebApplication, finalizing all configurations and services

// Configure the HTTP request pipeline (middleware)
if (app.Environment.IsDevelopment()) // Only enable Swagger in Development environment
{
    app.UseSwagger();     // Enables the generation of the OpenAPI JSON endpoint
    app.UseSwaggerUI();   // Enables the interactive Swagger UI frontend at /swagger
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS for security

app.UseAuthorization();    // Enables authorization middleware (checks user permissions)

app.MapControllers();      // Maps controller routes so they can respond to HTTP requests

app.Run();                 // Runs the application and starts listening for incoming requests