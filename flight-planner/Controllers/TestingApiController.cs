using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner.Controllers;

[Route("testing-api")]
[ApiController]
public class TestingApiController : ControllerBase
{
    private readonly IFlightService _flightService;

    public TestingApiController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [Route("clear")]
    [HttpPost]
    public IActionResult Clear()
    {
        _flightService.DeleteAll();
        
        return Ok();
    }
}