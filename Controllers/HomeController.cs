using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tema_1.DataBase;
using static Tema_1.DataBase.Reports;

namespace Tema_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly Reports _context;

        public HomeController(Reports context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        //The Db tracks down ,by it's "_context" ,an entity (table) with the same id specified. If not found. a "query" is made where an entity is asked to be given.=> entity|null
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //You can't have the same method or route /path for 2 methods in a controller
        [HttpPut("EditCity/{id}")]
        public IActionResult AddNewCity(int id, [FromBody] City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
    }
}

