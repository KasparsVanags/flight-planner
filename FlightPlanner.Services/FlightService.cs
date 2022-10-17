using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services;

public class FlightService : EntityService<Flight>, IFlightService
{
    public FlightService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public Flight? GetCompleteFlightById(int id)
    {
        return _context.Flights.Include(f => f.From)
            .Include(f => f.To)
            .SingleOrDefault(f => f.Id == id);
    }

    public bool Exists(Flight flight)
    {
        return _context.Flights.Any(f =>
            f.ArrivalTime == flight.ArrivalTime &&
            f.DepartureTime == flight.DepartureTime &&
            f.Carrier == flight.Carrier &&
            f.From.AirportCode == flight.From.AirportCode &&
            f.To.AirportCode == flight.To.AirportCode);
    }

    public List<Airport> SearchAirports(string query)
    {
        var results = _context.Airports.Where(x => x.AirportCode.ToLower().Trim().Contains(query) ||
                                                   x.Country.ToLower().Trim().Contains(query) ||
                                                   x.City.ToLower().Trim().Contains(query)).ToList();
        return results.DistinctBy(x => x.AirportCode).ToList();
    }

    public PageResult SearchFlightsByRequest(string from, string to, string departure)
    {
        return new PageResult
        {
            Items = _context.Flights
                .Include(x => x.From)
                .Include(x => x.To).ToList()
                .Where(x => x.From.AirportCode == from &&
                            x.To.AirportCode == to &&
                            DateTime.Parse(x.DepartureTime).Date == DateTime.Parse(departure).Date)
                .ToList()
        };
    }

    public void DeleteAll()
    {
        _context.Flights.RemoveRange(_context.Flights);
        _context.Airports.RemoveRange(_context.Airports);
        _context.SaveChanges();
    }
}