using flight_planner.Exceptions;

namespace flight_planner.Validators;

public static class FlightRequestValidator
{
    public static void Validate(SearchFlightsRequest search)
    {
        if (string.IsNullOrEmpty(search.From) ||
            string.IsNullOrEmpty(search.To) ||
            string.IsNullOrEmpty(search.DepartureDate) ||
            search.From.ToLower().Trim() == search.To.ToLower().Trim())
            throw new InvalidFlightRequestException();
    }

    public static void Format(SearchFlightsRequest search)
    {
        search.From = search.From.ToUpper().Trim();
        search.To = search.To.ToUpper().Trim();
    }
}