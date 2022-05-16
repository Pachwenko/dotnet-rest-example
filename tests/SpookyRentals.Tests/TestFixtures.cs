using Microsoft.EntityFrameworkCore;
using SpookyRentals.Models;
using Xunit;

namespace Tests;

/// <summary>
/// This test fixure creates the test database and adds some data to it.
/// All tests using this should have the [Collection("Transactional Tests")]
/// So that the tests cant effect each other by modifying the database.
///
/// See EF core documentation here https://docs.microsoft.com/en-us/ef/core/testing/testing-with-the-database
/// </summary>
public class TransactionalTestDatabaseFixture
{
    public string connectionString = "Server=localhost;Database=dotnet-rest-example-transactional-tests;Port=5433;User Id=pizzauser;Password=password";

    public SpookyRentalsContext CreateContext()
        => new SpookyRentalsContext(
            new DbContextOptionsBuilder<SpookyRentalsContext>()
                .UseNpgsql(connectionString)
                .Options);

    public TransactionalTestDatabaseFixture()
    {
        // Since this is used as a Collection fixture this is only called once, essentially a BeforeAll method
        using var context = CreateContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    /// <summary>
    /// Resets the datbase to the initial state. Make sure to inheirt IDisposable in your test class and call this.
    /// </summary>
    public void Cleanup()
    {
        RemoveRentals();
    }

    public void AddRentals()
    {
        using var context = CreateContext();
        context.AddRange(
            new Rental { Id = 1, Name = "Rental1", Description = "A spooky apartment", IsAvailable = true },
            new Rental { Id = 2, Name = "Rental2", Description = "A haunted house", IsAvailable = true }
        );
        context.SaveChanges();
    }

    public void RemoveRentals()
    {
        using var context = CreateContext();
        context.Rentals.RemoveRange(context.Rentals);
        context.SaveChanges();
    }

}

// This is the test class that all the collection tests will inherit from automatically
[CollectionDefinition("Transactional Tests")]
public class TransactionalTestsCollection : ICollectionFixture<TransactionalTestDatabaseFixture>
{
}
