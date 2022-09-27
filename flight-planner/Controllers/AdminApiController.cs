using flight_planner.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner.Controllers;

[Route("admin-api")]
[ApiController]
[Authorize]
public class AdminApiController : ControllerBase
{
    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = FlightStorage.GetFlightById(id);
        if (flight == null) return NotFound(id);
        return Ok(flight);
    }

    [Route("flights/{id}")]
    [HttpDelete]
    public IActionResult DeleteFlight(int id)
    {
        FlightStorage.DeleteFlight(id);
        return Ok(id);
    }

    [Route("flights")]
    [HttpPut]
    public IActionResult PutFlight(Flight flight)
    {
        try
        {
            FlightValidator.Format(flight);
            FlightValidator.Validate(flight);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(flight);
        }

        if (FlightStorage.AddFlight(flight) == null) return Conflict(flight);

        return Created("", flight);
    }
}