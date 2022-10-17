namespace flight_planner.Models.SearchFlightsRequestValidators;

public class RequestDepartureDateValidator : ISearchFlightsRequestValidator
{
    public bool IsValid(SearchFlightsRequest request)
    {
        return !string.IsNullOrEmpty(request.DepartureDate);
    }
}