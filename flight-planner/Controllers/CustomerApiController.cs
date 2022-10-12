using AutoMapper;
using flight_planner.Models;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Models.SearchFlightsRequestValidators;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly IEnumerable<ISearchFlightsRequestValidator> _flightsRequestValidators;
    private readonly IMapper _mapper;

    public CustomerApiController(IFlightService flightService,
        IEnumerable<ISearchFlightsRequestValidator> flightsRequestValidators, IMapper mapper)
    {
        _flightService = flightService;
        _flightsRequestValidators = flightsRequestValidators;
        _mapper = mapper;
    }

    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirports(string search)
    {
        search = search.Trim().ToLower();
        var airports = _flightService.SearchAirports(search);
        if (!airports.Any()) return NotFound();
        
        var response = airports.Select(x => _mapper.Map<AirportRequest>(x)).ToList();
        return Ok(response);
    }

    [Route("flights/search")]
    [HttpPost]
    public IActionResult SearchFlights(SearchFlightsRequest request)
    {
        if (!_flightsRequestValidators.All(x => x.IsValid(request))) return BadRequest();

        request.From = request.From.Trim().ToUpper();
        request.To = request.To.Trim().ToUpper();

        return Ok(_flightService.SearchFlightsByRequest(request));
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
}