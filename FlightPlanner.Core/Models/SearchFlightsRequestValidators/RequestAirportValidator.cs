namespace FlightPlanner.Core.Models.SearchFlightsRequestValidators;

public class RequestAirportValidator : ISearchFlightsRequestValidator
{
    public bool IsValid(SearchFlightsRequest request)
    {
        if (string.IsNullOrEmpty(request.From) || string.IsNullOrEmpty(request.To)) return false;

        return request.From.Trim().ToLower() != request.To.Trim().ToLower();
    }
}