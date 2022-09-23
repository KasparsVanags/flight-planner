using flight_planner.Exceptions;
using flight_planner.Extensions;

namespace flight_planner.Validators;

public static class AirportValidator
{
    public static void Validate(Airport airport)
    {
        if (string.IsNullOrEmpty(airport.AirportCode) ||
            string.IsNullOrEmpty(airport.City) ||
            string.IsNullOrEmpty(airport.Country))
            throw new InvalidAirportException();
    }

    public static Airport Format(Airport airport)
    {
        airport.AirportCode = airport.AirportCode.ToUpper().Trim();
        airport.City = airport.City.Trim().Capitalize();
        airport.Country = airport.Country.Trim().Capitalize();
        return airport;
    }
}