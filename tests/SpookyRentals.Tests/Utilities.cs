using SpookyRentals.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Tests;

/// <summary>
/// Inspired by https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0#customize-webapplicationfactory
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{

    private string _connectionString;

    public CustomWebApplicationFactory(string connectionString) : base()
    {
        this._connectionString = connectionString;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // remove the default database connection and use our special test database
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<SpookyRentalsContext>));

            services.Remove(descriptor);
            services.AddDbContext<SpookyRentalsContext>(options =>
            {
                options.UseNpgsql(_connectionString);
            });
        });
    }
}
