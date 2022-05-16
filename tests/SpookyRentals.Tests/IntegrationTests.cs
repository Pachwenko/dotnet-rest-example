using Xunit;
using Xunit.Abstractions;
using SpookyRentals.Models;

namespace Tests;

/// <summary>
/// Test against the Rentals API
/// Note that tests in a class do NOT run in parallel. In order to specify which tests run in parallel we specify the collection with [Collection("Transactional Tests")]
///     this automatically inherits from and sets up the TransactionalTestsCollection
///
/// These tests operate at the highest level. They are also slower than a Unit test
///     Read more about the different kinds of tests here https://docs.microsoft.com/en-us/ef/core/testing/
///     Or any other website on Integration vs Unit tests. Acceptance tests are usually a whole different thing
/// </summary>
[Collection("Transactional Tests")]
public class RentalApiIntegrationTests : IDisposable
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper output;

    public RentalApiIntegrationTests(ITestOutputHelper output, TransactionalTestDatabaseFixture fixture)
    {
        this.output = output; // use this instead of Console, e.x.: output.WriteLine("Hello World");
        Fixture = fixture;
        var application = new CustomWebApplicationFactory(fixture.connectionString);
        _client = application.CreateClient(); // we can use this to make requests to the API similar to how a frontend would
    }

    public TransactionalTestDatabaseFixture Fixture { get; }

    public void Dispose()
    {
        // This is called after each test method, essentially a BeforeEach method
        Fixture.Cleanup();
    }

    [Fact]
    public async void GetRentalById()
    {
        Fixture.AddRentals();
        var response = await _client.GetAsync("/rental/1");
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        var rental = await response.Content.ReadFromJsonAsync<Rental>();
        Assert.Equal("Rental1", rental.Name);
    }

    [Fact]
    public async void GetAllRentals()
    {
        Fixture.AddRentals();
        var response = await _client.GetAsync("/rental");
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        var rentals = await response.Content.ReadFromJsonAsync<Rental[]>();
        Assert.Equal("Rental1", rentals[0].Name);
        Assert.Equal("Rental2", rentals[1].Name);
    }

    [Fact]
    public async void CreateRental()
    {
        Rental newRental = new Rental { Name = "Rental1", Description = "Rental 1 Description", IsAvailable = true };
        var response = await _client.PostAsJsonAsync("/rental", newRental);
        response.EnsureSuccessStatusCode();
        response = await _client.GetAsync("/rental/1");
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        var rental = await response.Content.ReadFromJsonAsync<Rental>();
        Assert.Equal("Rental1", rental.Name);
    }

    [Fact]
    public async void DeleteRental()
    {
        Fixture.AddRentals();
        var response = await _client.DeleteAsync("/rental/1");
        response.EnsureSuccessStatusCode();
        response = await _client.GetAsync("/rental/1");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async void UpdateRental()
    {
        Fixture.AddRentals();
        var response = await _client.GetAsync("/rental/1");
        response.EnsureSuccessStatusCode();
        var rental = await response.Content.ReadFromJsonAsync<Rental>();
        Assert.Equal("Rental1", rental.Name);

        rental.Name = "Rental1 Updated";
        response = await _client.PutAsJsonAsync("/rental/1", rental);
        response.EnsureSuccessStatusCode();

        response = await _client.GetAsync("/rental/1");
        response.EnsureSuccessStatusCode();
        rental = await response.Content.ReadFromJsonAsync<Rental>();
        Assert.Equal("Rental1 Updated", rental.Name);
    }
}