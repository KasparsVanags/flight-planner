using flight_planner.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flight_planner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    private readonly FlightPlannerDbContext _context;

    public CustomerApiController(FlightPlannerDbContext context)
    {
        _context = context;
    }

    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirports(string search)
    {
        search = search.ToLower().Trim();
        var airports = _context.Airports
            .Where(x => x.AirportCode.ToLower().Trim().Contains(search) ||
                        x.Country.ToLower().Trim().Contains(search) ||
                        x.City.ToLower().Trim().Contains(search)).ToList();

        if (!airports.Any()) return NotFound();
        return Ok(airports);
    }

    [Route("flights/search")]
    [HttpPost]
    public IActionResult SearchFlights(SearchFlightsRequest search)
    {
        try
        {
            FlightRequestValidator.Format(search);
            FlightRequestValidator.Validate(search);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }

        var result = new PageResult
        {
            Items = _context.Flights
                .Include(x => x.From)
                .Include(x => x.To).ToList()
                .Where(x => x.From.AirportCode == search.From &&
                            x.To.AirportCode == search.To &&
                            DateTime.Parse(x.DepartureTime).Date == DateTime.Parse(search.DepartureDate).Date)
                .ToList()
        };

        return Ok(result);
    }

    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = _context.Flights
            .Include(x => x.From)
            .Include(x => x.To)
            .FirstOrDefault(x => x.Id == id);
        return flight == null ? NotFound(id) : Ok(flight);
    }
}