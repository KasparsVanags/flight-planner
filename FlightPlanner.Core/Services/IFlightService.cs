using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IFlightService : IEntityService<Flight>
{
    Flight? GetCompleteFlightById(int id);
    bool Exists(Flight flight);
    List<Airport> SearchAirports(string query);
    PageResult SearchFlightsByRequest(string from, string to, string departure);
    void DeleteAll();
}