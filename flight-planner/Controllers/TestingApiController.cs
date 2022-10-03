using Microsoft.AspNetCore.Mvc;

namespace flight_planner.Controllers;

[Route("testing-api")]
[ApiController]
public class TestingApiController : ControllerBase
{
    private readonly FlightPlannerDbContext _context;

    public TestingApiController(FlightPlannerDbContext context)
    {
        _context = context;
    }

    [Route("clear")]
    [HttpPost]
    public IActionResult Clear()
    {
        _context.Flights.RemoveRange(_context.Flights);
        _context.Airports.RemoveRange(_context.Airports);
        _context.SaveChanges();
        return Ok();
    }
}