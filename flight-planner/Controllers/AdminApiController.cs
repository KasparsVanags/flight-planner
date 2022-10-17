using AutoMapper;
using flight_planner.Models;
using FlightPlanner.Core.Extensions;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Models.AirportValidators;
using FlightPlanner.Core.Models.FlightValidators;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner.Controllers;

[Route("admin-api")]
[ApiController]
[Authorize]
public class AdminApiController : ControllerBase
{
    private static readonly object DbLock = new();
    private readonly IFlightService _flightService;
    private readonly IEnumerable<IAirportValidator> _airportValidators;
    private readonly IEnumerable<IFlightValidator> _flightValidators;
    private readonly IMapper _mapper;

    public AdminApiController(IFlightService flightService, IEnumerable<IFlightValidator> flightValidators,
        IEnumerable<IAirportValidator> airportValidators, IMapper mapper)
    {
        _flightService = flightService;
        _flightValidators = flightValidators;
        _airportValidators = airportValidators;
        _mapper = mapper;
    }

    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = _flightService.GetCompleteFlightById(id);
        if (flight == null) return NotFound(id);
        
        var response = _mapper.Map<FlightRequest>(flight);
        
        return Ok(response);
    }

    [Route("flights/{id}")]
    [HttpDelete]
    public IActionResult DeleteFlight(int id)
    {
        lock (DbLock)
        {
            var flight = _flightService.GetById(id);
            if (flight != null)
            {
                var result = _flightService.Delete(flight);
                
                if (!result.Success) return Problem(result.FormattedErrors);
            }
        }

        return Ok(id);
    }

    [Route("flights")]
    [HttpPut]
    public IActionResult PutFlight(Flight flight)
    {
        if (!_flightValidators.All(f => f.IsValid(flight) &&
                                        _airportValidators.All(a => a.IsValid(flight.From)) &&
                                        _airportValidators.All(a => a.IsValid(flight.To))))
        {
            return BadRequest();
        }

        flight = flight.Format();
        lock (DbLock)
        {
            if (_flightService.Exists(flight)) return Conflict("Identical flight already exists");
            var result = _flightService.Create(flight);
            
            if(!result.Success) return Problem(result.FormattedErrors);
        }
        
        var response = _mapper.Map<FlightRequest>(flight);
        
        return Created("", response);
    }
}