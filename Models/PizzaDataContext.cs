namespace ContosoPizza.Models;

using Microsoft.EntityFrameworkCore;

public class PizzaDataContext : DbContext
{
    public PizzaDataContext(DbContextOptions<PizzaDataContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // auto incrementing primary key
        modelBuilder.UseSerialColumns();
    }

    public DbSet<Pizza>? Pizzas { get; set; }
}