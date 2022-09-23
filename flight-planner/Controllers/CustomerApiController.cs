using flight_planner.Validators;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirports(string search)
    {
        var airports = FlightStorage.GetAirport(search);
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
            Items = FlightStorage.GetFlights(search)
        };

        return Ok(result);
    }

    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = FlightStorage.GetFlightById(id);
        return flight == null ? NotFound(id) : Ok(flight);
    }
}