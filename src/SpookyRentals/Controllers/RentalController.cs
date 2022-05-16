using SpookyRentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace SpookyRentals.Controllers;

/// <summary>
///     This is where you define your API endpoints.
///     Most of your application logic will either go here or be called from here.
/// </summary>
[ApiController]
[Route("[controller]")]
public class RentalController : ControllerBase
{
    private readonly SpookyRentalsContext db;
    public RentalController(SpookyRentalsContext context)
    {
        this.db = context;
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Rental>> GetAll()
    {

        if (db.Rentals == null)
        {
            // return an empty list if there are no rentals
            return new ActionResult<List<Rental>>(new List<Rental>());
        }

        return db.Rentals.Select(p => new Rental
        {
            Id = p.Id,
            Name = p.Name,
            IsAvailable = p.IsAvailable,
        }).ToList();
    }

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Rental> Get(int id)
    {
        var pizza = db.Rentals.Find(id);
        if (pizza is null)
            return NotFound();
        return pizza;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Rental pizza)
    {
        var newPizza = db.Rentals.Add(pizza);
        this.db.SaveChanges();
        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    // PUT action (update)
    [HttpPut("{id}")]
    public IActionResult Update(int id, Rental pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
        var existingPizza = db.Rentals.Find(id);
        if (existingPizza is null)
            return NotFound();
        if (!existingPizza.Equals(pizza))
        {
            existingPizza.Name = pizza.Name;
            existingPizza.IsAvailable = pizza.IsAvailable;
            db.SaveChanges();
        }
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = db.Rentals.Find(id);
        if (pizza is null)
            return NotFound();
        db.Rentals.Remove(pizza);
        db.SaveChanges();
        return NoContent();
    }

}