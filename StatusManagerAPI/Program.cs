using Microsoft.EntityFrameworkCore;
using StatusManagerAPI.Interfaces;
using StatusManagerAPI.Mappings;
using StatusManagerAPI.Middleware;
using StatusManagerAPI.Models;
using StatusManagerAPI.Services;
using StatusManagerAPI.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Status Manager API",
        Version = "v1",
        Description = "This API allows users to manage tasks and statuses.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Buket U. K.",
            Email = "your.email@example.com",
            Url = new Uri("https://your-portfolio.com")
        }
    });
});

builder.Services.AddDbContext<StatusManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(StatusProfile));

builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<StatusDtoValidator>();

builder.Services.AddScoped<IStatusService, StatusService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Status Manager API v1");
    });
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
