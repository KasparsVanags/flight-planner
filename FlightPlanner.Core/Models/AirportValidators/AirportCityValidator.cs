namespace FlightPlanner.Core.Models.AirportValidators;

public class AirportCityValidator : IAirportValidator
{
    public bool IsValid(Airport airport)
    {
        return !string.IsNullOrEmpty(airport.City);
    }
}