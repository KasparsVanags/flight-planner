using flight_planner.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flight_planner.Controllers;

[Route("admin-api")]
[ApiController]
[Authorize]
public class AdminApiController : ControllerBase
{
    private static readonly object DbLock = new();
    private readonly FlightPlannerDbContext _context;

    public AdminApiController(FlightPlannerDbContext context)
    {
        _context = context;
    }

    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = _context.Flights
            .Include(x => x.From)
            .Include(x => x.To)
            .FirstOrDefault(x => x.Id == id);
        if (flight == null) return NotFound(id);
        return Ok(flight);
    }

    [Route("flights/{id}")]
    [HttpDelete]
    public IActionResult DeleteFlight(int id)
    {
        lock (DbLock)
        {
            var flight = _context.Flights.FirstOrDefault(x => x.Id == id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }
        }

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

        lock (DbLock)
        {
            var flights = _context.Flights.Include(x => x.From)
                .Include(x => x.To).ToList();
            if (flights.Any(x => x.Equals(flight))) return Conflict(flight);
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        return Created("", flight);
    }
}