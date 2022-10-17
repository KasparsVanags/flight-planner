namespace FlightPlanner.Core.Models.FlightValidators;

public class FlightCarrierValidator : IFlightValidator
{
    public bool IsValid(Flight flight)
    {
        return !string.IsNullOrEmpty(flight.Carrier);
    }
}