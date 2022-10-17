namespace FlightPlanner.Core.Models.FlightValidators;

public class FlightAirportValidator : IFlightValidator
{
    public bool IsValid(Flight flight)
    {
        if (flight.From == null || flight.To == null) return false;

        return flight.From.AirportCode.Trim().ToLower() != flight.To.AirportCode.Trim().ToLower();
    }
}