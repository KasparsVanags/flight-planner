namespace FlightPlanner.Core.Models.AirportValidators;

public class AirportCodeValidator : IAirportValidator
{
    public bool IsValid(Airport airport)
    {
        return !string.IsNullOrEmpty(airport.AirportCode);
    }
}