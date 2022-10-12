namespace FlightPlanner.Core.Models.AirportValidators;

public interface IAirportValidator
{
    bool IsValid(Airport airport);
}