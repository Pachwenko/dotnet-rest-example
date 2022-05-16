using Microsoft.EntityFrameworkCore;
using SpookyRentals.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(
                "https://localhost:7053",
                "http://localhost:3000",
                "http://localhost:5059"
            );
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers();

// Set up the database connection using Npgsql
builder.Services.AddDbContext<SpookyRentalsContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PizzasDb"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }