using flight_planner.Exceptions;
using flight_planner.Extensions;

namespace flight_planner.Validators;

public static class FlightValidator
{
    public static void Validate(Flight flight)
    {
        if (string.IsNullOrEmpty(flight.Carrier) ||
            string.IsNullOrEmpty(flight.ArrivalTime) ||
            string.IsNullOrEmpty(flight.DepartureTime) ||
            flight.From.Equals(flight.To) ||
            DateTime.Parse(flight.DepartureTime).CompareTo(DateTime.Parse(flight.ArrivalTime)) >= 0)
            throw new InvalidFlightException();

        AirportValidator.Validate(flight.To);
        AirportValidator.Validate(flight.From);
    }

    public static void Format(Flight flight)
    {
        flight.Carrier = flight.Carrier.Trim().Capitalize();
        flight.From = AirportValidator.Format(flight.From);
        flight.To = AirportValidator.Format(flight.To);
    }
}