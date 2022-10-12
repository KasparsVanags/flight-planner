namespace FlightPlanner.Core.Models.FlightValidators;

public class FlightTimeValidator : IFlightValidator
{
    public bool IsValid(Flight flight)
    {
        if (string.IsNullOrEmpty(flight.DepartureTime) || string.IsNullOrEmpty(flight.ArrivalTime)) return false;

        var departure = DateTime.Parse(flight.DepartureTime);
        var arrival = DateTime.Parse(flight.ArrivalTime);
        return arrival > departure;
    }
}