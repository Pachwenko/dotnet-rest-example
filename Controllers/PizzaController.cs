using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaDataContext _context;
    public PizzaController(PizzaDataContext context)
    {
        this._context = context;
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() {
        // TODO: figure out how to fix this warning?
        // returns a 404 if the database is empty
        return this._context.Pizzas.Select(p => new Pizza {
            Id = p.Id,
            Name = p.Name,
            IsGlutenFree = p.IsGlutenFree,
        }).ToList();
    }

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = this._context.Pizzas.Find(id);
        if (pizza is null)
            return NotFound();
        return pizza;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza) {
        var newPizza = this._context.Pizzas.Add(pizza);
        this._context.SaveChanges();
        // PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza) {
        if (id != pizza.Id)
            return BadRequest();
        var existingPizza = this._context.Pizzas.Find(id);
        if (existingPizza is null)
            return NotFound();
        if (!existingPizza.Equals(pizza)) {
            existingPizza.Name = pizza.Name;
            existingPizza.IsGlutenFree = pizza.IsGlutenFree;
            this._context.SaveChanges();
        }
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var pizza = this._context.Pizzas.Find(id);
        if (pizza is null)
            return NotFound();
        this._context.Pizzas.Remove(pizza);
        return NoContent();
    }

}