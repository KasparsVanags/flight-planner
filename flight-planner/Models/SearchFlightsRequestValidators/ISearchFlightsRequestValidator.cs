namespace flight_planner.Models.SearchFlightsRequestValidators;

public interface ISearchFlightsRequestValidator
{
    bool IsValid(SearchFlightsRequest request);
}