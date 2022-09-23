using Microsoft.AspNetCore.Mvc;

namespace flight_planner.Controllers;

[Route("testing-api")]
[ApiController]
public class TestingApiController : ControllerBase
{
    [Route("clear")]
    [HttpPost]
    public IActionResult Clear()
    {
        FlightStorage.Clear();
        return Ok();
    }
}