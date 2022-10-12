namespace FlightPlanner.Core.Models.FlightValidators;

public interface IFlightValidator
{
    bool IsValid(Flight flight);
}