using FlightPlanner.Core.Extensions;

namespace FlightPlanner.Core.Models.AirportValidators;

public static class AirportFormatter
{
    public static Airport Format(this Airport airport)
    {
        airport.City = airport.City.Trim().Capitalize();
        airport.Country = airport.Country.Trim().Capitalize();
        airport.AirportCode = airport.AirportCode.Trim().ToUpper();
        
        return airport;
    }
}