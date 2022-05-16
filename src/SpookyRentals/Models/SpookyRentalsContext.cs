namespace SpookyRentals.Models;

using Microsoft.EntityFrameworkCore;

/// <summary>
///     This is where you define the interaction with your database.
/// </summary>
public class SpookyRentalsContext : DbContext
{
    public SpookyRentalsContext(DbContextOptions<SpookyRentalsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Use an auto incrementing integer for our primary keys
        // This is really fast and usually what you want. It is also less secure as it is vulnerable to enumeration attacks.
        // Alternatively you can use a GUID as your primary key OR implement anti-scraping to avoid enumeration attacks.
        modelBuilder.UseSerialColumns();
    }

    public DbSet<Rental> Rentals => Set<Rental>();
}