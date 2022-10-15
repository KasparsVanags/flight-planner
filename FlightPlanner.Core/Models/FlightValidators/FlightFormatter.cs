using FlightPlanner.Core.Extensions;
using FlightPlanner.Core.Models.AirportValidators;

namespace FlightPlanner.Core.Models.FlightValidators;

public static class FlightFormatter
{
    public static Flight Format(this Flight flight)
    {
        flight.Carrier = flight.Carrier.Trim().Capitalize();
        flight.From = flight.From.Format();
        flight.To = flight.To.Format();
        
        return flight;
    }
}