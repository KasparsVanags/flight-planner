namespace FlightPlanner.Core.Models.SearchFlightsRequestValidators;

public interface ISearchFlightsRequestValidator
{
    bool IsValid(SearchFlightsRequest request);
}