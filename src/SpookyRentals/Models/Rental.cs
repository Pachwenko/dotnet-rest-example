namespace SpookyRentals.Models;

/// <summary>
///     This is our rental model.
///     If you make changes here you must generate and run a database migration.
/// </summary>
public class Rental
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsAvailable { get; set; }
}